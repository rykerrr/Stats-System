using System;
using System.Collections.Generic;
using StatSystem.TakeOne;
using UnityEngine;

namespace DapperScripts.LevelingSystem
{
    public class LevelSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSystem = default;
        [SerializeField] private List<StatType> statTypesToGrow = new List<StatType>(); 
        
        private LevelSystem levelSystem = default;

        public LevelSystem LevelSystem => levelSystem ?? (levelSystem = new LevelSystem(
            0, statsSystem.StatsSystem, GetRequiredStatsFromStatTypes()));

        private List<StatBase> GetRequiredStatsFromStatTypes()
        {
            List<StatBase> statsToGrow = new List<StatBase>();

            foreach (var statType in statTypesToGrow)
            {
                statsToGrow.Add(statsSystem.StatsSystem.GetStat(statType));
            }

            return statsToGrow;
        }

        #region Debug
        [SerializeField] private int expToGive = default;

        [ContextMenu("Add exp, attempt to level up and grow stats")]
        private void GrowGivenStat()
        {
            LevelSystem.AddExp(expToGive);
        }
        
        [ContextMenu("Debug dump LevelSystem data")]
        private void DumpLevelSystemData()
        {   
            Debug.Log(LevelSystem.ToString());
        }
        #endregion
    }
}