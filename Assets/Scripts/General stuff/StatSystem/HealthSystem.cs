using System;
using System.Text;
using DapperScripts.LevelingSystem;
using UnityEngine;
using WizardGame.Timers;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    [Serializable]
    public class HealthSystem
    {
        [SerializeField] private int curHealth = default;
        
        public event Action<GameObject> onDeathEvent = delegate { };
        
        private DownTimer healTimer = default;
        private StatsSystem statsSys = default;
        private StatBase maxHealthStat = default;
        private StatBase vigorStat = default;
        private StatBase resolveStat = default;
        
        public int CurHealth => curHealth;

        private bool hasDied = default;
        
        private StringBuilder sb;
        private HealthSystem(StatsSystem statsSys)
        {
            Init(statsSys);
        }

        public void Init(StatsSystem statsSys)
        {
            sb = new StringBuilder();
            
            this.statsSys = statsSys;
            
            maxHealthStat = this.statsSys.GetStat(StatTypeDB.GetType("MaxHealth"));
            vigorStat = this.statsSys.GetStat(StatTypeDB.GetType("Vigor"));
            resolveStat = this.statsSys.GetStat(StatTypeDB.GetType("Resolve"));
            
            curHealth = maxHealthStat.ActualValue;
            
            InitAutoHealTimer();
        }

        private void InitAutoHealTimer()
        {
            healTimer = new DownTimer(1f / vigorStat.ActualValue);
            healTimer.OnTimerEnd += () => healTimer.SetTimer(1f / vigorStat.ActualValue);
            healTimer.OnTimerEnd += () => Heal(resolveStat.ActualValue, this);
            healTimer.OnTimerEnd += () => healTimer.Reset();
        }

        public void Tick()
        {
            healTimer.TryTick(Time.deltaTime);
        }

        public void TakeDamage(int dmg, GameObject damageSource = null)
        {
            curHealth = Mathf.Clamp(curHealth - dmg, 0, maxHealthStat.ActualValue);
            healTimer.Reset();
            
            if (!hasDied && curHealth == 0)
            {
                // Exp gain functions by last hit due to this
                Death(damageSource);
            }
        }

        public void Heal(int hp, object source)
        {
            if (curHealth == 0) return;
            
            curHealth = Mathf.Clamp(curHealth + hp, 0, maxHealthStat.ActualValue);
        }
        
        private void Death(GameObject source = null)
        {
            hasDied = true;
            
            onDeathEvent?.Invoke(source);
        }

        public override string ToString()
        {
            // cur health / max health | curhp/maxhp percentage | next heal time
            
            sb.Clear();
            sb.Append("Health/MaxHealth: ").Append(CurHealth).Append("/").Append(maxHealthStat.ActualValue).AppendLine();
            sb.Append("Health Percentage: ").Append(Math.Round((float) CurHealth / maxHealthStat.ActualValue, 3) * 100f)
                .Append("%").AppendLine();
            sb.Append("Next heal in: ").Append(healTimer.Time).AppendLine();

            return sb.ToString();
        }
    }
}
