using System;
using System.Collections.Generic;
using StatSystem.TakeOne;
using UnityEngine;

namespace DapperScripts.LevelingSystem
{
    [Serializable]
    public class LevelSystem
    {
        public event Action onLevelUp = delegate { };

        private List<StatBase> statsToGrow = new List<StatBase>();

        private int curLevel = 0;
        private int requiredExp = 0;
        private int curExp = 0;

        public float LevelExpPercentage => (float)curExp / requiredExp;
        public int CurLevel => curLevel;
        public int CurExp => curExp;
        public int RequiredExp => requiredExp;
        private bool LevelUpAvailable => curExp >= requiredExp;

        public LevelSystem(int curLevel, StatsSystem statsSystem, List<StatBase> statsToGrow)
        {
            Init(curLevel, statsSystem, statsToGrow);
        }

        public void Init(int curLevel, StatsSystem statsSystem, List<StatBase> statsToGrow)
        {
            this.statsToGrow = statsToGrow;
            
            curExp = CalculateTotalExpRequiredForLevel(curLevel);
            TryLevelUp();

            requiredExp = CalculateRequiredExp();
        }
        
        public void AddExp(int expToAdd)
        {
            curExp += expToAdd;

            if (LevelUpAvailable) TryLevelUp();
        }

        private void TryLevelUp()
        {
            while (LevelUpAvailable)
            {
                curExp -= requiredExp;
                curLevel++;
                
                requiredExp = CalculateRequiredExp();

                foreach (var stat in statsToGrow)
                {
                    stat.GrowByGrowthRate();
                }
                
                Debug.Log("Level up!");
                onLevelUp?.Invoke();
            }
        }
        
        private int CalculateRequiredExp()
        {
            return Mathf.RoundToInt(0.04f * Mathf.Pow(curLevel, 3) + 0.8f * curLevel
                                                                   + 2 * curLevel + 1);
        }

        private int CalculateTotalExpRequiredForLevel(int level)
        {
            int expSum = 0;

            for (int i = 0; i < level; i++)
            {
                expSum += Mathf.RoundToInt(0.04f * Mathf.Pow(i, 3) + 0.8f * i
                                                                          + 2 * i + 1);
            }

            return expSum;
        }

        public override string ToString()
        {
            return
                $"Level: {CurLevel} | Exp/Required Exp: {CurExp}/{RequiredExp} | Exp/ReqExp Percentage Wise: {LevelExpPercentage}";
        }
    }
}