using System;
using UnityEngine;

namespace StatSystem.TakeOne
{
    [Serializable]
    public class StatTypeDependency
    {
        [SerializeField] private string name = default;
        [SerializeField] private StatType statDependingOn = default;
        [SerializeField] private float statMultiplier = default;
        
        public StatType StatDependingOn => statDependingOn;
        public float StatMultiplier => statMultiplier;

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}