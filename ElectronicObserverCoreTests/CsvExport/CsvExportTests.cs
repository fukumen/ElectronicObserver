﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;
using ElectronicObserver.Database.Sortie;
using ElectronicObserver.Services;
using ElectronicObserver.Window.Tools.SortieRecordViewer;
using ElectronicObserver.Window.Tools.SortieRecordViewer.DataExport;
using Xunit;
using System.Threading;
using ElectronicObserver.Common.ContentDialogs.ExportProgress;
using ElectronicObserver.Translations;
using ElectronicObserver.Window.Wpf;
using ElectronicObserverTypes;

namespace ElectronicObserverCoreTests.CsvExport;

[Collection(DatabaseCollection.Name)]
public class CsvExportTests
{
	private DatabaseFixture Db { get; }
	private static ToolService ToolService { get; } = new(null!);
	private DataExportHelper DataExportHelper { get; } = new(new(true), ToolService);

	private static string BasePath =>
		Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "CsvExport");

	private static List<string> IgnoredHeaders { get; } = new()
	{
		// there are minor differences in the date format, but date isn't relevant
		"日付",
		// needs runtime data
		"海域",
		// needs runtime data
		"提督レベル",
	};

	private static List<string> IgnoredStatValues { get; } = new()
	{
		"回避",
		"索敵",
		"対潜",
		"運",
	};

	public CsvExportTests(DatabaseFixture db)
	{
		Db = db;

		CultureInfo c = new("ja-JP");

		Thread.CurrentThread.CurrentCulture = c;
		Thread.CurrentThread.CurrentUICulture = c;
	}

	private static async Task<string> GenerateCsv<TMap, TElement>(IEnumerable<TElement> data)
		where TMap : ClassMap<TElement>
	{
		CsvConfiguration config = new(CultureInfo.CurrentCulture);

		await using StringWriter writer = new();
		await using CsvWriter csv = new(writer, config);

		csv.Context.RegisterClassMap<TMap>();
		await csv.WriteRecordsAsync(data);

		return writer.ToString();
	}

	private void VerifyCsv(IReadOnlyList<string> logbookLines, IReadOnlyList<string> eoLines)
	{
		Assert.Equal(logbookLines.Count, eoLines.Count);

		string logbookHeaders = logbookLines[0];
		string eoHeaders = eoLines[0];

		for (int i = 1; i < eoLines.Count; i++)
		{
			VerifyCsvLine(logbookHeaders, eoHeaders, logbookLines[i], eoLines[i]);
		}
	}

	private void VerifyCsvLine(string logbookHeaderLine, string eoHeaderLine, string logbookLine, string eoLine)
	{
		string[] logbookHeaders = logbookHeaderLine.Split("\t");
		string[] logbookValues = logbookLine.Split("\t");
		string[] eoHeaders = eoHeaderLine.Split(",");
		string[] eoValues = eoLine.Split(",");

		// expanding the csv is fine as long as it's purely additive
		Assert.True(eoHeaders.Length >= logbookHeaders.Length);

		IShipDataMaster? attacker = null;
		IShipDataMaster? defender = null;

		for (int i = 0; i < eoHeaders.Length; i++)
		{
			Assert.Equal(logbookHeaders[i], eoHeaders[i]);

			string header = logbookHeaders[i];

			if (IgnoredHeaders.Contains(header)) continue;

			if (header == $"{CsvExportResources.PrefixAttacker}{CsvExportResources.Id}")
			{
				Enum.TryParse(eoValues[i], out ShipId attackerId);
				attacker = Db.MasterShips[attackerId];
			}

			if (header == $"{CsvExportResources.PrefixDefender}{CsvExportResources.Id}")
			{
				Enum.TryParse(eoValues[i], out ShipId defenderId);
				defender = Db.MasterShips[defenderId];
			}

			string logbookValue = logbookValues[i];
			string eoValue = eoValues[i];

			if (logbookValue != eoValue)
			{
				// logbook doesn't use the full names for certain abyssal ships
				// 空母ヲ級改 flagship -> 空母ヲ級改
				if (header.Contains("艦名") && eoValue.StartsWith(logbookValue))
				{
					continue;
				}

				if (header.StartsWith(CsvExportResources.PrefixAttacker)
					&& ShouldIgnore(attacker, header, logbookValue, eoValue))
				{
					continue;
				}

				if (header.StartsWith(CsvExportResources.PrefixDefender)
					&& ShouldIgnore(defender, header, logbookValue, eoValue))
				{
					continue;
				}
			}

			Assert.Equal(logbookValue, eoValue);
		}
	}

	private static bool ShouldIgnore(IShipDataMaster? ship, string header, string logbookValue, string eoValue)
	{
		// header should be Attacker.X or Defender.X
		string property = header.Split('.')[1];

		if (property == CsvExportResources.ShipType)
		{
			return logbookValue is "戦艦" && eoValue is "巡洋戦艦";
		}

		if (ship is null) return false;
		if (!ship.IsAbyssalShip) return false;

		static bool HasWrongRange(IShipDataMaster ship) => ship.ID is
			1560 or
			1586 or
			1615 or
			1617 or
			1618 or
			1762 or
			1764;

		if (HasWrongRange(ship) && property == CsvExportResources.Range)
		{
			// logbook has wrong range values for these
			return true;
		}

		bool isUnknownValue = string.IsNullOrEmpty(logbookValue);

		// logbook doesn't have all base values for abyssals, like asw, search, luck etc.
		// for those values it just takes 0 + equipment values
		// 74EO has these values for certain abyssals
		// so we assume that if the eo value is bigger, it's correct
		if (int.TryParse(logbookValue, out int logbookNumber) && int.TryParse(eoValue, out int eoNumber))
		{
			isUnknownValue = isUnknownValue || eoNumber > logbookNumber;
		}

		return isUnknownValue && IgnoredStatValues.Contains(property);
	}

	private static async Task<IReadOnlyList<string>> LoadLogbookLines(string fileName)
	{
		return await File.ReadAllLinesAsync(Path.Join(BasePath, fileName));
	}

	private static async Task<IReadOnlyList<string>> LoadEoLines<TMap, TElement>(
		string fileName,
		Func<ObservableCollection<SortieRecordViewModel>, ExportProgressViewModel, CancellationToken, Task<List<TElement>>> processData
	) where TMap : ClassMap<TElement>
	{
		Stream sortieRecordStream = File.OpenRead(Path.Join(BasePath, fileName));
		List<SortieRecord>? sortieRecords = await JsonSerializer.DeserializeAsync<List<SortieRecord>>(sortieRecordStream);

		Assert.NotNull(sortieRecords);

		ObservableCollection<SortieRecordViewModel> sorties = sortieRecords
			.Select(s => new SortieRecordViewModel(s, DateTime.UtcNow))
			.ToObservableCollection();

		List<TElement> dayShelling = await processData(sorties, new(), default);

		string csv = await GenerateCsv<TMap, TElement>(dayShelling);
		return csv.TrimEnd().Split("\r\n");
	}

	[Fact(DisplayName = "Day shelling - 砲撃戦1")]
	public async Task CsvExportTest1()
	{
		IReadOnlyList<string> logbookLines = await LoadLogbookLines("砲撃戦1.csv");
		IReadOnlyList<string> eoLines = await LoadEoLines<DayShellingExportMap, DayShellingExportModel>(
			"砲撃戦1.json", DataExportHelper.DayShelling);

		VerifyCsv(logbookLines, eoLines);
	}

	[Fact(DisplayName = "Day shelling - 砲撃戦2")]
	public async Task CsvExportTest2()
	{
		IReadOnlyList<string> logbookLines = await LoadLogbookLines("砲撃戦2.csv");
		IReadOnlyList<string> eoLines = await LoadEoLines<DayShellingExportMap, DayShellingExportModel>(
			"砲撃戦2.json", DataExportHelper.DayShelling);

		VerifyCsv(logbookLines, eoLines);
	}
}
