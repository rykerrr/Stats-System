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

        private void Awake()
        {
            // LevelSystem.Init(0, statsSystem.StatsSystem, GetRequiredStatsFromStatTypes());
            
            var healthSysBehaviour = GetComponent<HealthSystemBehaviour>();

            healthSysBehaviour.HealthSystem.onDeathEvent += source =>
            {
                // This code runs on the object that gets killed
                
                Debug.Log($"Murderer! {gameObject} was murdered by {source}");
                
                LevelSystemBehaviour sourceLvSysBehaviour = default;

                if (!ReferenceEquals(sourceLvSysBehaviour = source.GetComponent<LevelSystemBehaviour>(), null))
                {
                    var sourceLvSystem = sourceLvSysBehaviour.LevelSystem;
                    var lifeExpValue = LevelSystem.CalculateLifeExperienceValue(LevelSystem.CurLevel,
                        statsSystem.Entity.LifeValueExpMultiplier);
                    
                    Debug.Log($"Value of {gameObject.name}'s life in experience points: {lifeExpValue}");
                    
                    sourceLvSystem.AddExp(lifeExpValue);
                }
            };
        }

        private List<StatBase> GetRequiredStatsFromStatTypes()
        {
            List<StatBase> statsToGrow = new List<StatBase>();
            var statsSys = statsSystem.StatsSystem;
            
            foreach (var statType in statTypesToGrow)
            {
                statsToGrow.Add(statsSys.GetStat(statType));
            }

            return statsToGrow;
        }

        [Space(10), Header("Debug"), Space(10)]
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