using System;
using UnityEngine;
using WizardGame.Timers;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    [Serializable]
    public class HealthSystem
    {
        [SerializeField] private StatsSystemBehaviour statsSys = default;
        
        public event Action onDeathEvent = delegate { };
        
        private DownTimer healTimer = default;
        private StatBase maxHealthStat = default;
        private StatBase vigorStat = default;
        private StatBase resolveStat = default;
        
        [SerializeField] private int curHealth = default;
        private bool hasDied = default;
        
        public int CurHealth => curHealth;

        private HealthSystem()
        {
            maxHealthStat = statsSys.GetStat(StatTypeDB.GetType("MaxHealth"));
            vigorStat = statsSys.GetStat(StatTypeDB.GetType("Vigor"));
            resolveStat = statsSys.GetStat(StatTypeDB.GetType("Resolve"));
            
            curHealth = maxHealthStat.ActualValue;
            healTimer = new DownTimer(1f / vigorStat.ActualValue);

            healTimer.OnTimerEnd += () => healTimer.SetTimer(1f / vigorStat.ActualValue);
            healTimer.OnTimerEnd += () => Heal(resolveStat.ActualValue, this);
            healTimer.OnTimerEnd += () => healTimer.Reset();
            healTimer.OnTimerEnd += () => Debug.Log("Bonk!");
        }

        public void Tick()
        {
            healTimer.TryTick(Time.deltaTime);
        }

        public void TakeDamage(int dmg, object source)
        {
            curHealth = Mathf.Clamp(curHealth - dmg, 0, maxHealthStat.ActualValue);
            healTimer.Reset();
            
            if (!hasDied && curHealth == 0)
            {
                Death();
            }
        }

        public void Heal(int hp, object source)
        {
            if (curHealth == 0) return;
            
            curHealth = Mathf.Clamp(curHealth + hp, 0, maxHealthStat.ActualValue);
        }
        
        private void Death()
        {
            hasDied = true;
            onDeathEvent?.Invoke();
        }
    }
}
