﻿using ElectronicObserver.KancolleApi.Types.Interfaces;
using ElectronicObserver.KancolleApi.Types.Legacy.OpeningTorpedoRework;
using ElectronicObserver.Window.Tools.SortieRecordViewer.Sortie.Battle.Phase;

namespace ElectronicObserver.Window.Tools.SortieRecordViewer.Sortie.Battle;

public abstract class CombinedDayBattleData : DayBattleData
{
	protected PhaseShelling? Shelling3 { get; }

	protected CombinedDayBattleData(PhaseFactory phaseFactory, BattleFleets fleets, ICombinedDayBattleApiResponse battle)
		: base(phaseFactory, fleets, battle)
	{
		Shelling3 = PhaseFactory.Shelling(battle.ApiHougeki3, DayShellingPhase.Third);
	}

	protected CombinedDayBattleData(PhaseFactory phaseFactory, BattleFleets fleets, IOpeningTorpedoRework_CombinedDayBattleApiResponse battle)
		: base(phaseFactory, fleets, battle)
	{
		Shelling3 = PhaseFactory.Shelling(battle.ApiHougeki3, DayShellingPhase.Third);
	}
}
