﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Card
{
    public string cardID { get; private set; }
    public string[] name { get; private set; }
    public string[] description { get; private set; }
    public bool isGlobal { get; private set; } //global or regional

    //limits the amount of times the card can increase the reward
    public double maximumIncrementsDone { get; private set; }
    public double currentIncrementsDone { get; private set; }

    //increment per turn
    public SectorStatistics sectorConsequencesPerTurn { get; private set; }
    public double moneyRewardPerTurn { get; private set; }

    //reward if redeemed
    public SectorStatistics currentSectorConsequences { get; private set; }
    public double currentMoneyReward { get; private set; }

    public Card() { }

    public void TempMethod()
    {
        description = new string[2] { "dutch", "english" };
    }
    public Card(Card card)
    {
        cardID = card.cardID;
        name = new string[2] { card.name[0], card.name[1] };
        description = new string[2] { card.description[0], card.description[1] };
        isGlobal = card.isGlobal;
        maximumIncrementsDone = card.maximumIncrementsDone;
        currentIncrementsDone = card.currentIncrementsDone;
        sectorConsequencesPerTurn = new SectorStatistics(card.sectorConsequencesPerTurn);
        moneyRewardPerTurn = card.moneyRewardPerTurn;
        currentSectorConsequences = new SectorStatistics(card.currentSectorConsequences);
        currentMoneyReward = card.currentMoneyReward;        
    }

    public void increaseCurrentRewards()
    {
        currentMoneyReward += moneyRewardPerTurn;

        currentSectorConsequences.ModifyIncome(sectorConsequencesPerTurn.income);
        currentSectorConsequences.ModifyHappiness(sectorConsequencesPerTurn.happiness);
        currentSectorConsequences.ModifyEcoAwareness(sectorConsequencesPerTurn.ecoAwareness);
        currentSectorConsequences.ModifyProsperity(sectorConsequencesPerTurn.prosperity);
        currentSectorConsequences.pollution.ChangeAirPollution(sectorConsequencesPerTurn.pollution.airPollution);
        currentSectorConsequences.pollution.ChangeNaturePollution(sectorConsequencesPerTurn.pollution.naturePollution);
        currentSectorConsequences.pollution.ChangeWaterPollution(sectorConsequencesPerTurn.pollution.waterPollution);
        currentSectorConsequences.pollution.ChangeAirPollutionMutation(sectorConsequencesPerTurn.pollution.airPollutionIncrease);
        currentSectorConsequences.pollution.ChangeNaturePollutionMutation(sectorConsequencesPerTurn.pollution.naturePollutionIncrease);
        currentSectorConsequences.pollution.ChangeWaterPollutionMutation(sectorConsequencesPerTurn.pollution.waterPollutionIncrease);

        currentIncrementsDone++;
    }


    public void UseCardOnRegion(Region r, GameStatistics gs)
    {
        foreach (RegionSector rs in r.sectors)
            rs.ImplementStatisticValues(currentSectorConsequences, true);

        gs.ModifyMoney(currentMoneyReward, true);
    }

    public void UseCardOnCountry(List<Region> regions, GameStatistics gs)
    {
        foreach (Region r in regions)
        {
            foreach (RegionSector rs in r.sectors)
                rs.ImplementStatisticValues(currentSectorConsequences, true);
        }

        gs.ModifyMoney(currentMoneyReward, true);
    }
}