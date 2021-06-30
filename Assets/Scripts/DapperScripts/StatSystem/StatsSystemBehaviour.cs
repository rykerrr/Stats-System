using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    public class StatsSystemBehaviour : MonoBehaviour
    {
        [SerializeField] public EntityStats statsToInject = default;
        [SerializeField] private StatsSystem statsSystem = default;

        private StatsSystem StatsSystem => statsSystem ?? (statsSystem = new StatsSystem(statsToInject));
        private List<TimedStatModifier> timedModifiers = new List<TimedStatModifier>();

        private void Awake()
        {
            StatsSystem.Init(statsToInject);
        }

        private void Update()
        {
            for (int i = timedModifiers.Count - 1; i >= 0; i--)
            {
                timedModifiers[i].Tick(Time.deltaTime);
            }
        }

        public void AddTimedModifier(StatType statType, StatModifier modifier, float time)
        {
            var timedModifier = new TimedStatModifier(modifier, time);
            
            timedModifier.Timer.OnTimerEnd += () => RemoveModifier(statType, modifier);
            timedModifier.Timer.OnTimerEnd += () => timedModifiers.Remove(timedModifier);
            
            timedModifiers.Add(timedModifier);
        }
        
        public void AddModifier(StatType statType, StatModifier modifier)
            => StatsSystem.AddModifierTo(statType, modifier);

        public bool RemoveModifier(StatType statType, StatModifier modifier)
            => StatsSystem.RemoveModifierFrom(statType, modifier);
            
        public int RemoveModifiersFromSource(StatType statType, object source)
            => StatsSystem.RemoveModifierFromSource(statType, source);

        #region Debug things

        [SerializeField] private StatType key;
        [SerializeField] private StatModifier modifierToAddOrRemove;
        
        [ContextMenu("Add given modifier based on key")]
        public void AddModifierToStat()
        {
            StatsSystem.AddModifierTo(key, modifierToAddOrRemove);
        }

        [ContextMenu("Remove given modifier based on key")]
        public void RemoveModifierFromStat()
        {
            StatsSystem.RemoveModifierFrom(key, modifierToAddOrRemove);
        }

        [ContextMenu("Debug text dump")]
        public void DebugTextDump()
        {
            Debug.Log(statsSystem.ToString());
        }

        [ContextMenu("Check occurence amount of given modifier")]
        public void ModifierOccurrences()
        {
            Debug.Log(statsSystem.CheckModifierOccurrences(modifierToAddOrRemove));
        }
        #endregion
    }
}
