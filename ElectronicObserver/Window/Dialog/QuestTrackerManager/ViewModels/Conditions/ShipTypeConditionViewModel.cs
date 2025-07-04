﻿using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElectronicObserver.Core.Types;
using ElectronicObserver.Core.Types.Extensions;
using ElectronicObserver.Window.Dialog.QuestTrackerManager.Enums;
using ElectronicObserver.Window.Dialog.QuestTrackerManager.Models.Conditions;

namespace ElectronicObserver.Window.Dialog.QuestTrackerManager.ViewModels.Conditions;

public partial class ShipTypeConditionViewModel : ObservableObject, IConditionViewModel
{
	public ShipTypeConditionModel Model { get; set; }

	public ShipTypes SelectedType { get; set; } = ShipTypes.Destroyer;
	public IEnumerable<ShipTypes> AllTypes { get; }

	public ComparisonType ComparisonType { get; set; }
	public IEnumerable<ComparisonType> ComparisonTypes { get; }

	public bool MustBeFlagship { get; set; }

	public string Display =>
		$"({CountConditionDisplay}{LevelDisplay}) {ComparisonTypeDisplay} {Model.Count}{FlagshipConditionDisplay}";

	private string CountConditionDisplay => string.Join($" {QuestTrackerManagerResources.Operator_Or} ",
		Model.Types.Select(s => s.Display()));

	private string LevelDisplay => Model.Level switch
	{
		> 0 => $" Lv {Model.Level}+",
		_ => "",
	};

	private string ComparisonTypeDisplay => ComparisonType.Display();

	private string FlagshipConditionDisplay => MustBeFlagship switch
	{
		true => $"({QuestTrackerManagerResources.Flagship})",
		_ => ""
	};

	public ShipTypeConditionViewModel(ShipTypeConditionModel model)
	{
		Model = model;

		MustBeFlagship = Model.MustBeFlagship;
		ComparisonType = Model.ComparisonType;

		AllTypes = Enum.GetValues<ShipTypes>().Where(t => t is not ShipTypes.Unknown);
		ComparisonTypes = Enum.GetValues<ComparisonType>();

		Model.PropertyChanged += (_, _) =>
		{
			OnPropertyChanged(nameof(Display));
		};

		Model.Types.CollectionChanged += (_, _) =>
		{
			OnPropertyChanged(nameof(Display));
		};

		PropertyChanged += (_, args) =>
		{
			if (args.PropertyName is not nameof(MustBeFlagship)) return;

			Model.MustBeFlagship = MustBeFlagship;
		};

		PropertyChanged += (_, args) =>
		{
			if (args.PropertyName is not nameof(ComparisonType)) return;

			Model.ComparisonType = ComparisonType;
		};
	}

	[RelayCommand]
	private void AddType()
	{
		Model.Types.Add(SelectedType);
	}

	[RelayCommand]
	private void RemoveType(ShipTypes s)
	{
		Model.Types.Remove(s);
	}

	public bool ConditionMet(IFleetData fleet)
	{
		List<IShipData> ships = fleet.MembersInstance.OfType<IShipData>().ToList();

		bool flagshipCondition = !Model.MustBeFlagship ||
			Model.Types.Contains(ships[0].MasterShip.ShipType);

		IEnumerable<IShipData> validShips = Model.Types.Contains(ShipTypes.All) switch
		{
			true => ships,
			_ => ships.Where(s => Model.Types.Contains(s.MasterShip.ShipType)),
		};

		if (Model.Level > 0)
		{
			validShips = ships.Where(s => s.Level >= Model.Level);
		}

		bool countCondition = Compare(validShips.Count(), Model.Count, Model.ComparisonType);

		return flagshipCondition && countCondition;
	}

	private static bool Compare(int a, int b, ComparisonType comparisonType) =>
		comparisonType switch
		{
			ComparisonType.Equal => a == b,
			ComparisonType.LessOrEqual => a <= b,
			_ => a >= b
		};
}
