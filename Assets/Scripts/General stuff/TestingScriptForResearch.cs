using System;
using StatSystem.TakeOne;
using UnityEngine;
using WizardGame.Timers;

#pragma warning disable 0649
public class TestingScriptForResearch : MonoBehaviour
{
    [SerializeField] private StatsSystemBehaviour statsSys = default;
    [SerializeField] private StatType typeForModification;
    [SerializeField] private StatModifier modifier;

    [ContextMenu("Add timed modifier with 20 sec with given modifier for given type")]
    public void TestMethod()
    {
        statsSys.StatsSystem.AddTimedModifier(typeForModification, modifier, 10);
    }
}
