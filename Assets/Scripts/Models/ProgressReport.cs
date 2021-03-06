﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*because the progress report needs to be reset each month in order to keep track of the new changes, 2 new reports have to be
 * created in order to keep track of the old turn. This is required for when the player saves and quits the game and on a later time
 * loads in the game again. The old reports will then be able to initialize the monthly (and yearly report if necessary).
*/
[Serializable]
public class ProgressReport
{
    public string[] reportRegions { get; private set; }
    public double[] oldIncome { get; private set; }
    public double[] oldHappiness { get; private set; }
    public double[] oldEcoAwareness { get; private set; }
    public double[] oldProsperity { get; private set; }
    public double[] oldPollution { get; private set; }

    public List<RegionAction>[] completedActions { get; private set; }
    public List<GameEvent>[] newEvents { get; private set; }
    public List<GameEvent>[] completedEvents { get; private set; }

    public ProgressReport()
    {
        reportRegions = new string[4] { "Noord Nederland", "Oost Nederland", "West Nederland", "Zuid Nederland" };
        oldIncome = new double[4] { 0, 0, 0, 0 };
        oldHappiness = new double[4] { 0, 0, 0, 0 };
        oldEcoAwareness = new double[4] { 0, 0, 0, 0 };
        oldProsperity = new double[4] { 0, 0, 0, 0 };
        oldPollution = new double[4] { 0, 0, 0, 0 };
        completedActions = new List<RegionAction>[] { new List<RegionAction>(), new List<RegionAction>(), new List<RegionAction>(), new List<RegionAction>()};
        newEvents = new List<GameEvent>[] { new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>() };
        completedEvents = new List<GameEvent>[] { new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>() };
    }

    //creates a new Progressreport as a copy to avoid references
    public ProgressReport(ProgressReport p)
    {
        reportRegions = new string[4] { "Noord Nederland", "Oost Nederland", "West Nederland", "Zuid Nederland" };
        oldIncome = new double[4] { p.oldIncome[0], p.oldIncome[1], p.oldIncome[2], p.oldIncome[3] };
        oldHappiness = new double[4] { p.oldHappiness[0], p.oldHappiness[1], p.oldHappiness[2], p.oldHappiness[3] };
        oldEcoAwareness = new double[4] { p.oldEcoAwareness[0], p.oldEcoAwareness[1], p.oldEcoAwareness[2], p.oldEcoAwareness[3] };
        oldProsperity = new double[4] { p.oldProsperity[0], p.oldProsperity[1], p.oldProsperity[2], p.oldProsperity[3] };
        oldPollution = new double[4] { p.oldPollution[0], p.oldPollution[1], p.oldPollution[2], p.oldPollution[3] };
        completedActions = new List<RegionAction>[] { new List<RegionAction>(), new List<RegionAction>(), new List<RegionAction>(), new List<RegionAction>() };
        newEvents = new List<GameEvent>[] { new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>() };
        completedEvents = new List<GameEvent>[] { new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>() };
        for (int i = 0; i < 4; i++)
        {
            foreach (RegionAction r in p.completedActions[i])
                completedActions[i].Add(new RegionAction(r));
            foreach (GameEvent e in p.newEvents[i])
                newEvents[i].Add(new GameEvent(e));
            foreach (GameEvent e in p.completedEvents[i])
                completedEvents[i].Add(new GameEvent(e));
        }
    }

    #region UpdateReport
    public void UpdateStatistics(List<MapRegion> regions)
    {
        foreach (MapRegion region in regions)
        {
            for (int i = 0; i < reportRegions.Length; i++)
            {
                if (region.name[0] == reportRegions[i])
                {
                    oldIncome[i] = region.statistics.income;
                    oldHappiness[i] = region.statistics.happiness;
                    oldEcoAwareness[i] = region.statistics.ecoAwareness;
                    oldProsperity[i] = region.statistics.prosperity;
                    oldPollution[i] = region.statistics.avgPollution;
                    completedActions[i] = new List<RegionAction>();
                    newEvents[i] = new List<GameEvent>();
                    completedEvents[i] = new List<GameEvent>();
                    break;
                }
            }
        }
    }

    public void AddCompletedAction(MapRegion region, RegionAction action)
    {
        for (int i = 0; i < reportRegions.Length; i++)
        {
            if (region.name[0] == reportRegions[i])
            {
                completedActions[i].Add(action);
                break;
            }
        }
    }

    public void AddNewGameEvent(MapRegion region, GameEvent gameEvent)
    {
        for (int i = 0; i < reportRegions.Length; i++)
        {
            if (region.name[0] == reportRegions[i])
            {
                newEvents[i].Add(gameEvent);
                break;
            }
        }
    }

    public void AddCompletedGameEvent(MapRegion region, GameEvent gameEvent)
    {
        for (int i = 0; i < reportRegions.Length; i++)
        {
            if (region.name[0] == reportRegions[i])
            {
                completedEvents[i].Add(gameEvent);
                break;
            }
        }
    }
    #endregion
}