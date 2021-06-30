using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    [CreateAssetMenu(fileName = "Base Dependant Stat Type", menuName = "Stats/Default Dependant Stat")]
    public class DependantStatType : StatType
    {
        [SerializeField] private List<StatType> statsDependingOn = new List<StatType>();
        
        [SerializeField] private new string name = default;
        [SerializeField] private int value = default;

        public List<StatType> StatsDependingOn => statsDependingOn;
        public override string Name => name;
        public override int Value => value;

        public override string ToString()
        {
            return
                $"Default Dependant Stat Type Name: {Name} | Default Dependant Stat Type Value: {Value}\nValues depending on: {StatsDependingOn}";
        }
    }
}
