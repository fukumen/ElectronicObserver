﻿using System.Collections.Generic;
using ElectronicObserver.Core.Types.Extensions;

namespace ElectronicObserver.Core.Types.AntiAir;

public class AntiAirCutInCondition
{
	public List<ShipId>? Ships { get; init; }
	public List<ShipClass>? ShipClasses { get; init; }

	public int HighAngle { get; init; }
	public int HighAngleDirector { get; init; }
	public int HighAngleWithoutDirector { get; init; }
	public int AaDirector { get; init; }
	public int Radar { get; init; }
	public int AntiAirRadar { get; init; }
	public int MainGunLarge { get; init; }
	public int MainGunLargeFcr { get; init; }
	public int AaShell { get; init; }
	public int AaGun { get; init; }
	public int AaGun3Aa { get; init; }
	public int AaGun4Aa { get; init; }
	public int AaGun5Aa { get; init; }
	public int AaGun6Aa { get; init; }
	public int AaGun3To8Aa { get; init; }

	/// <summary>
	/// AA >= 9
	/// </summary>
	public int AaGunConcentrated { get; init; }
	public int AaGunPompom { get; init; }
	public int AaRocketBritish { get; init; }
	public int AaRocketMod { get; init; }
	public int HighAngleMusashi { get; init; }
	public int HighAngleAmerican { get; init; }
	public int HighAngleAmericanKai { get; init; }
	public int HighAngleAmericanGfcs { get; init; }
	public int RadarGfcs { get; init; }
	public int HighAngleAtlanta { get; init; }
	public int HighAngleAtlantaGfcs { get; init; }
	public int HighAngleConcentrated { get; init; }
	public int RadarYamato { get; init; }

	/// <summary>
	/// 35.6cm連装砲改三(ダズル迷彩仕様) < br/>
	/// 35.6cm連装砲改四
	/// </summary>
	public int HarunaGun { get; init; }

	/// <summary>
	/// 12.7cm連装砲C型改三H
	/// </summary>
	public int HarusameGun { get; init; }

	/// <summary>
	/// 25mm対空機銃増備
	/// </summary>
	public int AaGunShigure { get; init; }

	public int Radar4AaOrMore { get; init; }

	/// <summary>
	/// 10cm連装高角砲改+高射装置改
	/// </summary>
	public int HatsuzukiGun { get; init; }

	/// <summary>
	/// 10cm連装高角砲改 (＋高射装置改)
	/// </summary>
	public int AkizukiGunKai { get; init; }

	/// <summary>
	/// 10cm連装高角砲改
	/// </summary>
	public int AkizukiPotatoGun { get; init; }

	public int Aafd94 { get; init; }

	private bool ShipCondition(IShipData ship, AntiAirCutIn antiAirCutIn)
	{
		if (ship.MasterShip.ShipClassTyped is ShipClass.Akizuki && antiAirCutIn.Id is 5 or 7 or 8) return false;
		if (ship.MasterShip.ShipId is ShipId.MayaKaiNi && antiAirCutIn.Id is 13) return false;

		if (Ships is null && ShipClasses is null) return true;
		if (Ships is not null && Ships.Contains(ship.MasterShip.ShipId)) return true;
		if (ShipClasses is not null && ShipClasses.Contains(ship.MasterShip.ShipClassTyped)) return true;

		return false;
	}

	public bool CanBeActivatedBy(IShipData ship, AntiAirCutIn antiAirCutIn)
	{
		if (!ShipCondition(ship, antiAirCutIn)) return false;

		if (!ship.HasHighAngleGun(HighAngle)) return false;
		if (!ship.HasHighAngleDirectorGun(HighAngleDirector)) return false;
		if (!ship.HasHighAngleWithoutDirectorGun(HighAngleWithoutDirector)) return false;
		if (!ship.HasDirector(AaDirector)) return false;
		if (!ship.HasRadar(Radar)) return false;
		if (!ship.HasAirRadar(AntiAirRadar)) return false;
		if (!ship.HasMainGunLarge(MainGunLarge)) return false;
		if (!ship.HasMainGunLargeFcr(MainGunLargeFcr)) return false;
		if (!ship.HasAaShell(AaShell)) return false;
		if (!ship.HasAaGun(AaGun)) return false;
		if (!ship.HasAaGun(AaGun3Aa, 3)) return false;
		if (!ship.HasAaGun(AaGun4Aa, 4)) return false;
		if (!ship.HasAaGun(AaGun5Aa, 5)) return false;
		if (!ship.HasAaGun(AaGun6Aa, 6)) return false;
		if (!ship.HasAaGun(AaGun3To8Aa, 3, 8)) return false;
		if (!ship.HasAaGun(AaGunConcentrated, 9)) return false;
		if (!ship.HasPompom(AaGunPompom)) return false;
		if (!ship.HasAaRocketBritish(AaRocketBritish)) return false;
		if (!ship.HasAaRocketMod(AaRocketMod)) return false;
		if (!ship.HasHighAngleMusashi(HighAngleMusashi)) return false;
		if (!ship.HasHighAngleAmerican(HighAngleAmerican)) return false;
		if (!ship.HasHighAngleAmericanKai(HighAngleAmericanKai)) return false;
		if (!ship.HasHighAngleAmericanGfcs(HighAngleAmericanGfcs)) return false;
		if (!ship.HasRadarGfcs(RadarGfcs)) return false;
		if (!ship.HasHighAngleAtlanta(HighAngleAtlanta)) return false;
		if (!ship.HasHighAngleAtlantaGfcs(HighAngleAtlantaGfcs)) return false;
		if (!ship.HasHighAngleConcentrated(HighAngleConcentrated)) return false;
		if (!ship.HasYamatoRadar(RadarYamato)) return false;
		if (!ship.HasHarunaGun(HarunaGun)) return false;
		if (!ship.HasHarusameGun(HarusameGun)) return false;
		if (!ship.HasShigureAaGun(AaGunShigure)) return false;
		if (!ship.HasAirRadar(Radar4AaOrMore, 4)) return false;
		if (!ship.HasHatsuzukiGun(HatsuzukiGun)) return false;
		if (!ship.HasAkizukiGunKai(AkizukiGunKai)) return false;
		if (!ship.HasAkizukiPotatoGun(AkizukiPotatoGun)) return false;
		if (!ship.HasAafd94(Aafd94)) return false;

		return true;
	}
}
