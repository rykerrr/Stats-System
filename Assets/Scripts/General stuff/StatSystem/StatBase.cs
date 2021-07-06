using System.Collections.Generic;
using System;
using UnityEngine;

namespace StatSystem.TakeOne
{
    public abstract class StatBase
    {
        protected Action statWasModified = delegate { };

        protected List<StatModifier> modifiers = new List<StatModifier>();
        
        protected int baseValue = default;
        protected float growthRate = default;
        protected int actualValue = default;
        protected bool isDirty = true;
        
        public IReadOnlyList<StatModifier> Modifiers => modifiers.AsReadOnly();
        
        public Action StatWasModified
        {
            get => statWasModified;
            set => statWasModified = value;
        }

        // Can be made private
        // TODO: look at why it was kept as public though my only assumption is that it's for the level system
        // and we don't need that setup anymore
        public int BaseValue
        {
            get => baseValue;
            set
            {
                baseValue = value;

                StatWasModified.Invoke();
                isDirty = true;
            }
        }

        public float GrowthRate => growthRate;

        public int ActualValue
        {
            get
            {
                if (isDirty)
                {
                    actualValue = CalculateValue();
                    isDirty = false;
                }

                return actualValue;
            }
        }

        public bool IsDirty => isDirty;
        protected int OriginalValue { get; }


        public StatBase(StatType defType, float growthRate)
        {
            baseValue = defType.Value;
            OriginalValue = defType.Value;
            
            this.growthRate = growthRate;
        }

        protected int ApplyModifiers(int valueToApplyTo)
        {
            actualValue = valueToApplyTo;
            float additiveModSum = 0;

            modifiers.Sort(StatModifier.CompareModifierOrder);

            for (int i = 0; i < Modifiers.Count; i++)
            {
                var mod = Modifiers[i];

                switch (mod.Type)
                {
                    case ModifierType.Flat:
                    {
                        actualValue += (int) Math.Round(mod.Value);
                        break;
                    }
                    case ModifierType.PercentAdditive:
                    {
                        additiveModSum += mod.Value;

                        if (i == Modifiers.Count - 1 || Modifiers[i + 1].Type != ModifierType.PercentAdditive)
                        {
                            actualValue = (int) Mathf.Round(actualValue * (1 + additiveModSum));
                        }

                        break;
                    }
                    case ModifierType.PercentFinalMultiplicative:
                    {
                        actualValue = (int) Math.Round(actualValue * (1 + mod.Value));

                        break;
                    }
                }
            }

            return actualValue;
        }

        public abstract int CalculateValue();

        public void GrowByGrowthRate()
        {
            BaseValue += Mathf.RoundToInt(OriginalValue * GrowthRate);
        }
        
        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            StatWasModified.Invoke();

            modifiers.Add(mod);
        }

        public bool RemoveModifier(StatModifier mod)
        {
            isDirty = true;
            StatWasModified.Invoke();

            return modifiers.Remove(mod);
        }

        public int RemoveModifiersFromSource(object source)
            => modifiers.RemoveAll(x => x.Source == source);
    }
}