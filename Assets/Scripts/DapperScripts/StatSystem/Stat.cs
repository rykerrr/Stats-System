#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    public class Stat : StatBase
    {
        public Stat(StatType defType) : base(defType)
        {
            
        }
        
        public override int CalculateValue() => ApplyModifiers(baseValue);
    }
}