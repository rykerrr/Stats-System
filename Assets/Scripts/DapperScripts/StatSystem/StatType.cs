using UnityEngine;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    public abstract class StatType : ScriptableObject
    {
        public abstract string Name { get; }
        public abstract int Value { get; }

        public abstract override string ToString();
    }
}
