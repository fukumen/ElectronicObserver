﻿using System.Collections.Generic;
using ElectronicObserver.KancolleApi.Types.ApiReqSortie.Battle;
using ElectronicObserver.KancolleApi.Types.Legacy.OpeningTorpedoRework;
using ElectronicObserver.Window.Tools.SortieRecordViewer.Sortie.Battle.Phase;

namespace ElectronicObserver.Window.Tools.SortieRecordViewer.Sortie.Battle;

/// <summary>
/// 通常艦隊 vs 通常艦隊 昼戦 <br />
/// api_req_sortie/battle
/// </summary>
public sealed class BattleNormalDay : DayBattleData
{
	public override string Title => ConstantsRes.Title_NormalDay;

	public BattleNormalDay(PhaseFactory phaseFactory, BattleFleets fleets, ApiReqSortieBattleResponse battle)
		: base(phaseFactory, fleets, battle)
	{
		EmulateBattle();
	}

	public BattleNormalDay(PhaseFactory phaseFactory, BattleFleets fleets, OpeningTorpedoRework_ApiReqSortieBattleResponse battle)
		: base(phaseFactory, fleets, battle)
	{
		EmulateBattle();
	}

	protected override IEnumerable<PhaseBase?> AllPhases()
	{
		yield return Initial;
		yield return Searching;
		yield return JetBaseAirAttack;
		yield return JetAirBattle;
		yield return BaseAirAttack;
		yield return FriendlySupportInfo;
		yield return FriendlyAirBattle;
		yield return AirBattle;
		yield return Support;
		yield return OpeningAsw;
		yield return OpeningTorpedo;
		yield return Shelling1;
		yield return Shelling2;
		yield return ClosingTorpedo;
	}
}
