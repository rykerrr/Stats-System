using System;
using StatSystem.TakeOne;
using UnityEngine;

#pragma warning disable 0649
public class TestingScriptForResearch : MonoBehaviour
{
    [SerializeField] private StatsSystemBehaviour statsSys = default;
    [SerializeField] private StatType typeForModification;
    [SerializeField] private StatModifier modifier;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var modifierThingy = new StatModifier(ModifierType.Flat, 4, this);
            var thingyToModify = StatTypeDB.GetType("Mana");
            
            statsSys.AddModifier(thingyToModify, modifierThingy);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            statsSys.RemoveModifier(typeForModification, modifier);
        }
    }
}
