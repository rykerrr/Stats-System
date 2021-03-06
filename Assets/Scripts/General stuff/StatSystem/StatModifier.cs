using System;
using UnityEngine;

namespace StatSystem.TakeOne
{
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private ModifierType type;
        [SerializeField] private float value;
        
        private object source = default;
        
        public ModifierType Type => type;
        public float Value => value;
        public object Source => source;

        public StatModifier(ModifierType type, float value, object source)
        {
            this.type = type;
            this.value = value;
            this.source = source;
        }

        public static int CompareModifierOrder(StatModifier x, StatModifier y)
        {
            if ((int) x.type > (int) y.type) return 1;
            if ((int) x.type == (int) y.type) return 0;

            return -1;
        }

        public override string ToString()
        {
            return $"Type: {Type}, Value: {Value}";
        }
    }
}