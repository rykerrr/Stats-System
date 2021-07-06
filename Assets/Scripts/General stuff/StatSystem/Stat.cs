#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    public class Stat : StatBase
    {
        public Stat(StatType defType, float growthRate) : base(defType, growthRate)
        {
            
        }
        
        public override int CalculateValue() => ApplyModifiers(baseValue);
    }
}