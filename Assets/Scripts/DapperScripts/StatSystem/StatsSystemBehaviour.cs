using UnityEngine;
using WizardGame.Timers;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    public class StatsSystemBehaviour : MonoBehaviour
    {
        [SerializeField] public EntityStats statsToInject = default;
        [SerializeField] private StatsSystem statsSystem = default;

        public StatsSystem StatsSystem => statsSystem ?? (statsSystem = new StatsSystem(statsToInject
                                                                    , TimerTickerSingleton.Instance));

        private void Awake()
        {
            StatsSystem.Init(statsToInject);
            
            DebugTextDump();
        }

        // public void AddTimedModifier(StatType statType, StatModifier statModifier, float time)
        // {
        //     var timeTicker = TimerTickerSingleton.Instance;
        //     Debug.Log(timeTicker, TimerTickerSingleton.Instance);
        //     
        //     DownTimer newTimer = new DownTimer(time);
        //
        //     timeTicker.Timers.Add(newTimer);
        //     AddModifier(statType, statModifier);
        //
        //     newTimer.OnTimerEnd += () => RemoveModifier(statType, statModifier);
        //     newTimer.OnTimerEnd += () => timeTicker.RemoveTimer(newTimer);
        // } 
        
        // public void AddModifier(StatType statType, StatModifier modifier)
        //     => StatsSystem.AddModifierTo(statType, modifier);
        //
        // public bool RemoveModifier(StatType statType, StatModifier modifier)
        //     => StatsSystem.RemoveModifierFrom(statType, modifier);
        //     
        // public int RemoveModifiersFromSource(StatType statType, object source)
        //     => StatsSystem.RemoveModifierFromSource(statType, source);
        //
        // public StatBase GetStat(StatType statType)
        //     => StatsSystem.GetStat(statType);
        
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
