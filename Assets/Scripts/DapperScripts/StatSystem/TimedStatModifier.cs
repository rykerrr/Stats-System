using UnityEngine;
using WizardGame.Timers;

namespace StatSystem.TakeOne
{
    public class TimedStatModifier
    {
        private DownTimer timer = default;
        private StatModifier modifier = default;

        public DownTimer Timer => timer;
        public StatModifier Modifier => modifier;

        public TimedStatModifier(StatModifier modifier, float time)
        {
            timer = new DownTimer(time);
            this.modifier = modifier;
        }

        public bool Tick(float deltaTime)
            => timer.TryTick(deltaTime);
        
    }
}