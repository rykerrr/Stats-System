using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    [CreateAssetMenu(fileName = "New Entity Stats", menuName = "Stats/Entity Stats")]
    public class EntityStats : ScriptableObject
    {
        [SerializeField] private List<StatType> defaultTypes = new List<StatType>();

        public List<StatType> DefaultTypes
        {
            get
            {
                // Keep this despite the fact it's sorting a SO?
                defaultTypes.Sort(ReturnBaseStatFirst);

                return defaultTypes;
            }
        }

        public int FirstDepStatIndex
        {
            get
            {
                firstDepStatIndex = defaultTypes.FindIndex(x => x.GetType() == typeof(DependantStatType));

                return firstDepStatIndex;
            }
        }

        private int firstDepStatIndex = default;
        
        private int ReturnBaseStatFirst(StatType x, StatType y)
        {
            var a = x.GetType() == typeof(DependantStatType) ? 1 : -1;
            var b = y.GetType() == typeof(DependantStatType) ? 1 : -1;
            
            return a - b;
        }
    }
}
