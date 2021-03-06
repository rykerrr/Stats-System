using UnityEngine;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    [CreateAssetMenu(fileName = "Base Stat Type", menuName = "Stats/Default Base Stat")]
    public class BaseStatType : StatType
    {
        [SerializeField] private new string name = default;
        [SerializeField] private int value = default;

        public override string Name
        {
            get => name;
            protected set => name = value;
        }
        public override int Value => value;
        
        public override string ToString()
        {
            return $"Default Stat Type Name: {Name} | Default Stat Type Value: {Value}";
        }
    }
}
