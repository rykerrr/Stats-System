using System;

namespace StatSystem.TakeOne
{
    [Serializable]
    public class StatDependency
    {
        private StatBase statDependingOn;
        private float statMultiplier;

        public StatBase StatDependingOn => statDependingOn;
        public float StatMultiplier => statMultiplier;

        public StatDependency(StatBase statDependingOn, float statMultiplier)
        {
            this.statDependingOn = statDependingOn;
            this.statMultiplier = statMultiplier;
        }
    }
}