﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ElectronicObserverTypes.AntiAir;

/// <summary>
/// <see href="https://docs.google.com/spreadsheets/d/1agGoLv57g5eOXLXtNIKHRoBYy61OQYxibWP6Vi_DMuY" />
/// </summary>
[DebuggerDisplay("Id = {Id}")]
public record AntiAirCutIn
{
	private static List<AntiAirCutIn> CutIns { get; } = new()
	{
		new()
		{
			Id = 0,
			FixedBonus = 0,
			VariableBonus = 1,
			Rate = 1,
			Conditions = new(),
		},
		new()
		{
			Id = 1,
			FixedBonus = 7,
			VariableBonus = 1.7,
			Rate = 0.643,
			Conditions = new()
			{
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Akizuki,
					},
					HighAngle = 2,
					Radar = 1,
				},
			},
		},
		new()
		{
			Id = 2,
			FixedBonus = 6,
			VariableBonus = 1.7,
			Rate = 0.574,
			Conditions = new()
			{
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Akizuki,
					},
					HighAngle = 1,
					Radar = 1,
				},
			},
		},
		new()
		{
			Id = 3,
			FixedBonus = 4,
			VariableBonus = 1.6,
			Rate = 0.495,
			Conditions = new()
			{
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Akizuki,
					},
					HighAngle = 2,
				},
			},
		},
		new()
		{
			Id = 4,
			FixedBonus = 6,
			VariableBonus = 1.5,
			Rate = 0.514,
			Conditions = new()
			{
				new()
				{
					MainGunLarge = 1,
					AaShell = 1,
					AaDirector = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 5,
			FixedBonus = 4,
			VariableBonus = 1.5,
			Rate = 0.544,
			Conditions = new()
			{
				new()
				{
					HighAngleDirector = 2,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 6,
			FixedBonus = 4,
			VariableBonus = 1.45,
			Rate = 0.396,
			Conditions = new()
			{
				new()
				{
					MainGunLarge = 1,
					AaShell = 1,
					AaDirector = 1,
				},
			},
		},
		new()
		{
			Id = 7,
			FixedBonus = 3,
			VariableBonus = 1.35,
			Rate = 0.445,
			Conditions = new()
			{
				new()
				{
					HighAngle = 1,
					AaDirector = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 8,
			FixedBonus = 4,
			VariableBonus = 1.4,
			Rate = 0.495,
			Conditions = new()
			{
				new()
				{
					HighAngleDirector = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 9,
			FixedBonus = 2,
			VariableBonus = 1.3,
			Rate = 0.396,
			Conditions = new()
			{
				new()
				{
					HighAngle = 1,
					AaDirector = 1,
				},
			},
		},
		new()
		{
			Id = 10,
			FixedBonus = 8,
			VariableBonus = 1.65,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.MayaKaiNi,
					},
					HighAngle = 1,
					AaGunConcentrated = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 11,
			FixedBonus = 6,
			VariableBonus = 1.5,
			Rate = 0.544,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.MayaKaiNi,
					},
					HighAngle = 1,
					AaGunConcentrated = 1,
				},
			},
		},
		new()
		{
			Id = 12,
			FixedBonus = 3,
			VariableBonus = 1.25,
			Rate = 0.445,
			Conditions = new()
			{
				new()
				{
					AaGunConcentrated = 1,
					// the actual condition is 1 AA gun with at least 3 AA and 1 concentrated AA gun (at least 9 AA)
					// the concentrated AA gun will obviously also satisfy the 3 AA gun condition
					AaGun3Aa = 2,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 13,
			FixedBonus = 4,
			VariableBonus = 1.35,
			Rate = 0.346,
			Conditions = new()
			{
				new()
				{
					HighAngle = 1,
					AaGunConcentrated = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 14,
			FixedBonus = 4,
			VariableBonus = 1.45,
			Rate = 0.623,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.IsuzuKaiNi,
					},
					HighAngle = 1,
					AaGun = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 15,
			FixedBonus = 3,
			VariableBonus = 1.3,
			Rate = 0.534,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.IsuzuKaiNi,
					},
					HighAngle = 1,
					AaGun = 1,
				},
			},
		},
		new()
		{
			Id = 16,
			FixedBonus = 4,
			VariableBonus = 1.4,
			Rate = 0.613,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.KasumiKaiNiB,
						ShipId.YuubariKaiNi,
					},
					HighAngle = 1,
					AaGun = 1,
					Radar = 1,
				},
			},
		},
		new()
		{
			Id = 17,
			FixedBonus = 2,
			VariableBonus = 1.25,
			Rate = 0.564,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.KasumiKaiNiB,
						ShipId.YuubariKaiNi,
					},
					HighAngle = 1,
					AaGun = 1,
				},
			},
		},
		new()
		{
			Id = 18,
			FixedBonus = 2,
			VariableBonus = 1.2,
			Rate = 0.584,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.SatsukiKaiNi,
					},
					AaGunConcentrated = 1,
				},
			},
		},
		new()
		{
			Id = 19,
			FixedBonus = 5,
			VariableBonus = 1.45,
			Rate = 0.564,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.KinuKaiNi,
					},
					HighAngleWithoutDirector = 1,
					AaGunConcentrated = 1,
				},
			},
		},
		new()
		{
			Id = 20,
			FixedBonus = 3,
			VariableBonus = 1.25,
			Rate = 0.643,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.KinuKaiNi,
					},
					AaGunConcentrated = 1,
				},
			},
		},
		new()
		{
			Id = 21,
			FixedBonus = 5,
			VariableBonus = 1.45,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.YuraKaiNi,
					},
					HighAngle = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 22,
			FixedBonus = 2,
			VariableBonus = 1.2,
			Rate = 0.643,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.FumizukiKaiNi,
					},
					AaGunConcentrated = 1,
				},
			},
		},
		new()
		{
			Id = 23,
			FixedBonus = 1,
			VariableBonus = 1.05,
			Rate = 0.792,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.UIT25,
						ShipId.I504,
					},
					AaGun3To8Aa = 1,
				},
			},
		},
		new()
		{
			Id = 24,
			FixedBonus = 3,
			VariableBonus = 1.25,
			Rate = 0.613,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.TenryuuKaiNi,
						ShipId.TatsutaKaiNi,
					},
					HighAngle = 1,
					AaGun3To8Aa = 1,
				},
			},
		},
		new()
		{
			Id = 25,
			FixedBonus = 7,
			VariableBonus = 1.55,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.IseKai,
						ShipId.IseKaiNi,
						ShipId.HyuugaKai,
						ShipId.HyuugaKaiNi,
					},
					AaRocketMod = 1,
					AntiAirRadar = 1,
					AaShell = 1,
				},
			},
		},
		new()
		{
			Id = 26,
			FixedBonus = 6,
			VariableBonus = 1.4,
			Rate = 0.554,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.YamatoKaiNi,
						ShipId.YamatoKaiNiJuu,
						ShipId.MusashiKaiNi,
					},
					HighAngleMusashi = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 27,
			FixedBonus = 5,
			VariableBonus = 1.55,
			Rate = 0.519,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.OoyodoKai,
					},
					HighAngleMusashi = 1,
					AaRocketMod = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 28,
			FixedBonus = 4,
			VariableBonus = 1.4,
			Rate = 0.554,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.IseKai,
						ShipId.IseKaiNi,
						ShipId.HyuugaKai,
						ShipId.HyuugaKaiNi,
						ShipId.MusashiKai,
						ShipId.MusashiKaiNi,
					},
					AaRocketMod = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 29,
			FixedBonus = 5,
			VariableBonus = 1.55,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.IsokazeBKai,
						ShipId.HamakazeBKai,
					},
					HighAngle = 1,
					AntiAirRadar = 1,
				},
			},
		},
		new()
		{
			Id = 30,
			FixedBonus = 3,
			VariableBonus = 1.3,
			Rate = 0.495,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.TenryuuKaiNi,
						ShipId.GotlandKai,
						ShipId.Gotlandandra,
					},
					HighAngle = 3,
				},
			},
		},
		new()
		{
			Id = 31,
			FixedBonus = 2,
			VariableBonus = 1.25,
			Rate = 0.495,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.TenryuuKaiNi,
					},
					HighAngle = 2,
				},
			},
		},
		new()
		{
			Id = 32,
			FixedBonus = 3,
			VariableBonus = 1.2,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.KongouKaiNi,
						ShipId.KongouKaiNiC,
						ShipId.HieiKaiNi,
						ShipId.HieiKaiNiC,
						ShipId.HarunaKaiNi,
						ShipId.HarunaKaiNiB,
						ShipId.HarunaKaiNiC,
						ShipId.KirishimaKaiNi,
						ShipId.Jervis,
						ShipId.JervisKai,
						ShipId.Janus,
						ShipId.JanusKai,
						ShipId.Nelson,
						ShipId.NelsonKai,
						ShipId.Warspite,
						ShipId.WarspiteKai,
						ShipId.ArkRoyal,
						ShipId.ArkRoyalKai,
					},
					MainGunLargeFcr = 1,
					AaGunPompom = 1,
				},
				new()
				{
					Ships = new()
					{
						ShipId.KongouKaiNi,
						ShipId.KongouKaiNiC,
						ShipId.HieiKaiNi,
						ShipId.HieiKaiNiC,
						ShipId.HarunaKaiNi,
						ShipId.HarunaKaiNiB,
						ShipId.HarunaKaiNiC,
						ShipId.KirishimaKaiNi,
						ShipId.Jervis,
						ShipId.JervisKai,
						ShipId.Janus,
						ShipId.JanusKai,
						ShipId.Nelson,
						ShipId.NelsonKai,
						ShipId.Warspite,
						ShipId.WarspiteKai,
						ShipId.ArkRoyal,
						ShipId.ArkRoyalKai,
					},
					AaRocketBritish = 1,
					AaGunPompom = 1,
				},
				new()
				{
					Ships = new()
					{
						ShipId.KongouKaiNi,
						ShipId.KongouKaiNiC,
						ShipId.HieiKaiNi,
						ShipId.HieiKaiNiC,
						ShipId.HarunaKaiNi,
						ShipId.HarunaKaiNiB,
						ShipId.HarunaKaiNiC,
						ShipId.KirishimaKaiNi,
						ShipId.Jervis,
						ShipId.JervisKai,
						ShipId.Janus,
						ShipId.JanusKai,
						ShipId.Nelson,
						ShipId.NelsonKai,
						ShipId.Warspite,
						ShipId.WarspiteKai,
						ShipId.ArkRoyal,
						ShipId.ArkRoyalKai,
					},
					AaRocketBritish = 2,
				},
			},
		},
		new()
		{
			Id = 33,
			FixedBonus = 3,
			VariableBonus = 1.35,
			Rate = 0.415,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.GotlandKai,
						ShipId.Gotlandandra,
					},
					HighAngle = 1,
					AaGun4Aa = 1,
				},
			},
		},
		new()
		{
			Id = 34,
			FixedBonus = 7,
			VariableBonus = 1.6,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Fletcher,
					},
					HighAngleAmericanGfcs = 2,
				},
			},
		},
		new()
		{
			Id = 35,
			FixedBonus = 6,
			VariableBonus = 1.55,
			Rate = 0.544,
			Conditions = new()
			{
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Fletcher,
					},
					HighAngleAmerican = 1,
					HighAngleAmericanGfcs = 1,
				},
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Fletcher,
					},
					HighAngleAmericanKai = 1,
					HighAngleAmericanGfcs = 1,
				},
			},
		},
		new()
		{
			Id = 36,
			FixedBonus = 6,
			VariableBonus = 1.55,
			Rate = 0.495,
			Conditions = new()
			{
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Fletcher,
					},
					HighAngleAmerican = 2,
					RadarGfcs = 1,
				},
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Fletcher,
					},
					HighAngleAmerican = 1,
					HighAngleAmericanKai = 1,
					RadarGfcs = 1,
				},
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Fletcher,
					},
					HighAngleAmericanKai = 2,
					RadarGfcs = 1,
				},
			},
		},
		new()
		{
			Id = 37,
			FixedBonus = 4,
			VariableBonus = 1.45,
			Rate = 0.396,
			Conditions = new()
			{
				new()
				{
					ShipClasses = new()
					{
						ShipClass.Fletcher,
					},
					HighAngleAmericanKai = 2,
				},
			},
		},
		new()
		{
			Id = 38,
			FixedBonus = 10,
			VariableBonus = 1.85,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlantaGfcs = 2,
				},
			},
		},
		new()
		{
			Id = 39,
			FixedBonus = 10,
			VariableBonus = 1.7,
			Rate = 0.55,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlanta = 1,
					HighAngleAtlantaGfcs = 1,
				},
			},
		},
		new()
		{
			Id = 40,
			FixedBonus = 10,
			VariableBonus = 1.7,
			Rate = 0.55,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlanta = 2,
					RadarGfcs = 1,
				},
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlanta = 1,
					HighAngleAtlantaGfcs = 1,
					RadarGfcs = 1,
				},
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlantaGfcs = 2,
					RadarGfcs = 1,
				},
			},
		},
		new()
		{
			Id = 41,
			FixedBonus = 9,
			VariableBonus = 1.65,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlanta = 2,
					RadarGfcs = 1,
				},
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlanta = 1,
					HighAngleAtlantaGfcs = 1,
				},
				new()
				{
					Ships = new()
					{
						ShipId.Atlanta,
						ShipId.AtlantaKai,
					},
					HighAngleAtlantaGfcs = 2,
				},
			},
		},
		new()
		{
			Id = 42,
			FixedBonus = 10,
			VariableBonus = 1.65,
			Rate = 0.643,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.YamatoKaiNi,
						ShipId.YamatoKaiNiJuu,
						ShipId.MusashiKaiNi,
					},
					HighAngleConcentrated = 2,
					RadarYamato = 1,
					AaGun6Aa = 1,
				},
			},
		},
		new()
		{
			Id = 43,
			FixedBonus = 8,
			VariableBonus = 1.6,
			Rate = 0.594,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.YamatoKaiNi,
						ShipId.YamatoKaiNiJuu,
						ShipId.MusashiKaiNi,
					},
					HighAngleConcentrated = 2,
					RadarYamato = 1,
				},
			},
		},
		new()
		{
			Id = 44,
			FixedBonus = 6,
			VariableBonus = 1.6,
			Rate = 0.544,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.YamatoKaiNi,
						ShipId.YamatoKaiNiJuu,
						ShipId.MusashiKaiNi,
					},
					HighAngleConcentrated = 1,
					RadarYamato = 1,
					AaGun6Aa = 1,
				},
			},
		},
		new()
		{
			Id = 45,
			FixedBonus = 5,
			VariableBonus = 1.55,
			Rate = 0.495,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.YamatoKaiNi,
						ShipId.YamatoKaiNiJuu,
						ShipId.MusashiKaiNi,
					},
					HighAngleConcentrated = 1,
					RadarYamato = 1,
				},
			},
		},
		new()
		{
			Id = 46,
			FixedBonus = 8,
			VariableBonus = 1.55,
			Rate = 0.495,
			Conditions = new()
			{
				new()
				{
					Ships = new()
					{
						ShipId.HarunaKaiNiB,
					},
					HarunaGun = 1,
					AaGunConcentrated = 1,
					AntiAirRadar = 1,
				},
			},
		},
	};

	public static List<AntiAirCutIn> AllCutIns => CutIns
		.OrderBy(a => ApiPriority.IndexOf(a.Id))
		.ToList();

	private static List<int> ApiPriority { get; } = new()
	{
		38,
		39,
		40,
		42,
		41,
		10,
		43,
		46,
		11,
		25,
		1,
		34,
		44,
		26,
		4,
		2,
		35,
		36,
		27,
		45,
		19,
		21,
		29,
		16,
		14,
		3,
		5,
		6,
		28,
		37,
		33,
		30,
		8,
		13,
		15,
		7,
		20,
		24,
		32,
		12,
		31,
		17,
		18,
		22,
		9,
		23,
		0,
	};

	public int Id { get; init; }
	public int FixedBonus { get; init; }
	public double VariableBonus { get; init; }
	private double? Rate { get; init; }

	/// <summary>
	/// Null when unknown
	/// </summary>
	public List<AntiAirCutInCondition>? Conditions { get; init; }

	public string ValueDisplay => Rate switch
	{
		double => $"({Rate:P0} - {FixedBonus} - x{VariableBonus})",
		_ => $"(??? - {FixedBonus} - x{VariableBonus})",
	};

	private AntiAirCutIn()
	{

	}

	public static AntiAirCutIn FromId(int id) => AllCutIns
		.FirstOrDefault(a => a.Id == id)
		?? new AntiAirCutIn
		{
			Id = id,
		};

	private bool CanBeActivatedBy(IShipData ship)
	{
		if (!Conditions.Any()) return true;
		if (Conditions.Any(c => c.CanBeActivatedBy(ship))) return true;

		return false;
	}

	public static List<AntiAirCutIn> PossibleCutIns(IShipData ship) => AllCutIns
		.Where(a => a.CanBeActivatedBy(ship))
		.OrderBy(a => ApiPriority.IndexOf(a.Id))
		.ToList();

	public static List<AntiAirCutIn> PossibleCutIns(IEnumerable<IShipData?> ships) => ships
		.Where(s => s is not null)
		.SelectMany(PossibleCutIns!)
		.DistinctBy(a => a.Id)
		.OrderBy(a => ApiPriority.IndexOf(a.Id))
		.ToList();
}
