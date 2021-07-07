using System;
using System.Collections.Generic;
using StatSystem.TakeOne;
using UnityEngine;

#pragma warning disable 0649
public class OnCollissionDealDamage : MonoBehaviour
{
    [SerializeField] private int damageToDeal = 4;
    
    private readonly List<HealthSystemBehaviour> healthSystemBehavs = new List<HealthSystemBehaviour>();

    private void Update()
    {
        if (healthSystemBehavs.Count > 0)
        {
            healthSystemBehavs.ForEach(x => x.HealthSystem.TakeDamage(damageToDeal, gameObject));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthSystemBehaviour healthSysBehav;

        var canAddHealthSys = !ReferenceEquals(healthSysBehav = other.GetComponent<HealthSystemBehaviour>(), null)
            && !healthSystemBehavs.Contains(healthSysBehav);

        if (canAddHealthSys)
        {
            healthSystemBehavs.Add(healthSysBehav);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HealthSystemBehaviour healthSysBehav;

        if (!ReferenceEquals(healthSysBehav = other.GetComponent<HealthSystemBehaviour>(), null))
        {
            healthSystemBehavs.Remove(healthSysBehav);
        }    
    }
}
