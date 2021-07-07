using UnityEngine;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    public class StatsSystemBehaviour : MonoBehaviour
    {
        [SerializeField] public EntityStats statsToInject = default;
        
        private StatsSystem statsSystem = default;

        public StatsSystem StatsSystem => statsSystem ?? (statsSystem = new StatsSystem(statsToInject
                                                                    , TimerTickerSingleton.Instance));

        public EntityStats Entity => statsToInject;

        [Space(10), Header("Debug"), Space(10)]
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
