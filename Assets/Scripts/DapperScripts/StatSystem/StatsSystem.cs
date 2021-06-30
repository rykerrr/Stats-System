using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using StatSystem.SerializableTypes;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    [Serializable]
    public class StatsSystem
    {
        [SerializeField] private SerializableDictionary<StatType, StatBase> stats = new SerializableDictionary<StatType, StatBase>();
        
        private StringBuilder sb;
        
        public StatsSystem(EntityStats entityToLoad)
        {
            Init(entityToLoad);
        }

        public void Init(EntityStats entityToLoad)
        {
            stats.Clear();
            
            var defStatList = entityToLoad.DefaultTypes;
            var firstDepIndex = entityToLoad.FirstDepStatIndex;

            for (int i = 0; i < firstDepIndex; i++)
            {
                stats.Add(defStatList[i], new Stat(defStatList[i]));
            }

            for (int i = firstDepIndex; i < defStatList.Count; i++)
            {
                var depStatBase = (DependantStatType) defStatList[i];
                var depStat = CreateDependantStat(depStatBase);

                stats.Add(depStatBase, depStat);
            }
        }

        private DependantStat CreateDependantStat(DependantStatType depStatBase)
        {
            DependantStat depStat = new DependantStat(depStatBase);
            
            foreach (var statDepType in depStatBase.StatsDependingOn)
            {
                if (stats.ContainsKey(statDepType))
                {
                    depStat.AddStatDependency(stats[statDepType]);
                }
                else
                {
                    // But it could be a DependantStat too...
                    StatBase statDependency = new Stat(depStatBase);

                    stats.Add(statDepType, statDependency);
                    depStat.AddStatDependency(statDependency);
                }
            }

            return depStat;
        }

        private StatBase CreateStat(StatType statType)
        {
            StatBase retStat = default;
            
            switch (statType)
            {
                case BaseStatType baseType:
                {
                    retStat = new Stat(baseType);
                    break;
                }
                case DependantStatType dependantType:
                {
                    retStat = CreateDependantStat(dependantType);
                    break;
                }
            }

            return retStat;
        }
        
        public void AddModifierTo(StatType statType, StatModifier modifierToAdd)
        {
            stats?[statType].AddModifier(modifierToAdd);
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
