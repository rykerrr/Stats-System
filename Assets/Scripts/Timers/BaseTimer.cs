using System;

namespace WizardGame.Timers
{
    public abstract class BaseTimer : ITimer
    {
        public bool IsTimerEnabled { get; private set; } = true;

        protected Action onTimerEnd = delegate {  };

        public Action OnTimerEnd
        {
            get => onTimerEnd;
            set => onTimerEnd = value;
        }

        public float Time { get; protected set; }
        
        public void EnableTimer() => IsTimerEnabled = true;
        public void DisableTimer() => IsTimerEnabled = false;
        public abstract void Reset();

        public abstract bool TryTick(float time);
    }
}