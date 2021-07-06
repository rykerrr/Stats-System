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

        #region Debug

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
