using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using StatSystem.SerializableTypes;
using WizardGame.Timers;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    [Serializable]
    public class StatsSystem
    {
        [SerializeField]
        private SerializableDictionary<StatType, StatBase> stats = new SerializableDictionary<StatType, StatBase>();
        private SerializableDictionary<StatType, StatBase> Stats => stats;
        
        private StringBuilder sb = new StringBuilder();
        private TimerTickerSingleton timeTicker = default;

        public StatsSystem(EntityStats entityToLoad, TimerTickerSingleton timeTicker)
        {
            this.timeTicker = timeTicker;
            
            Init(entityToLoad);
        }

        public void Init(EntityStats entityToLoad)
        {
            stats.Clear();

            InitializeStats(entityToLoad);
        }

        private void InitializeStats(EntityStats entityToLoad)
        {
            var defEntityStatList = entityToLoad.DefaultEntityStats;
            var firstDepIndex = entityToLoad.FirstDepStatIndex;

            for (int i = 0; i < firstDepIndex; i++)
            {
                var entStat = defEntityStatList[i];
                
                stats.Add(entStat.StatType, new Stat(entStat.StatType, entStat.GrowthRate));
            }

            for (int i = firstDepIndex; i < defEntityStatList.Count; i++)
            {
                // Debug.Log(defEntityStatList[i] + " | " + defEntityStatList[firstDepIndex]);
                
                var entityStat = defEntityStatList[i];
                var depStat = CreateDependantStat(entityStat, defEntityStatList);

                stats.Add(entityStat.StatType, depStat);
            }
        }

        private DependantStat CreateDependantStat(EntityStat entityStat, List<EntityStat> listForGrowthRates)
        {
            var depStatBase = (DependantStatType)entityStat.StatType;
            DependantStat depStat = new DependantStat(depStatBase, entityStat.GrowthRate);

            foreach (var statTypeDep in depStatBase.StatsDependingOn)
            {
                if (stats.ContainsKey(statTypeDep.StatDependingOn))
                {
                    depStat.AddStatDependency(stats[statTypeDep.StatDependingOn], statTypeDep.StatMultiplier);
                }
                else
                {
                    // could probably be done in a much better way
                    // i need the growth rates here of the stats the DependantStat is depending on
                    float depStatGrowthRate = listForGrowthRates.Find(x => x.StatType == depStatBase).GrowthRate;
                    StatBase statDependency = new Stat(depStatBase, depStatGrowthRate);

                    stats.Add(statTypeDep.StatDependingOn, statDependency);
                    depStat.AddStatDependency(statDependency, statTypeDep.StatMultiplier);
                }
            }

            return depStat;
        }

        public StatBase GetStat(StatType statType)
        {
            if (!Stats.ContainsKey(statType))
            {
                #if UNITY_EDITOR
                    Debug.LogWarning("StatType doesn't exist in the stats system. " +
                                     "Perhaps it's not on the Entity or there has been a problem with " +
                                     "loading the stats system?");
                #endif

                return null;
            }
            
            return Stats[statType];
        }
        
        public void AddModifierTo(StatType statType, StatModifier modifierToAdd)
        {
            stats?[statType].AddModifier(modifierToAdd);
        }

        public void AddTimedModifier(StatType statType, StatModifier statModifier, float time)
        {
            DownTimer newTimer = new DownTimer(time);
        
            timeTicker.Timers.Add(newTimer);
            AddModifierTo(statType, statModifier);
        
            newTimer.OnTimerEnd += () => RemoveModifierFrom(statType, statModifier);
            newTimer.OnTimerEnd += () => timeTicker.RemoveTimer(newTimer);
        } 
        
        public bool RemoveModifierFrom(StatType statType, StatModifier modifierToRemove)
        {
            return stats.ContainsKey(statType) && stats[statType].RemoveModifier(modifierToRemove);
        }

        public int RemoveModifierFromSource(StatType statType, object source)
        {
            return stats.ContainsKey(statType) ? stats[statType].RemoveModifiersFromSource(source) : 0;
        }

        public override string ToString()
        {
            if (ReferenceEquals(sb, null)) sb = new StringBuilder();

            foreach (var stat in stats)
            {
                sb.Append($"{stat.Key.Name} : {stat.Value.ActualValue.ToString()} | {stat.Value}").AppendLine();
            }

            var retStr = sb.ToString();
            sb.Clear();

            return retStr;
        }

#if UNITY_EDITOR
        public int CheckModifierOccurrences(StatModifier modifier)
        {
            int occurrences = default;

            foreach (var stat in stats)
            {
                occurrences += stat.Value.Modifiers.Count(x => ReferenceEquals(x, modifier));
            }

            return occurrences;
        }
#endif
    }
}