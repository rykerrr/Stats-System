using System;
using UnityEngine;

namespace StatSystem.TakeOne
{
    public class HealthSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehaviour = default;
        [SerializeField] private HealthSystem healthSystem = default;

        public HealthSystem HealthSystem => healthSystem;

        private void Awake()
        {
            healthSystem.Init(statsSysBehaviour.StatsSystem);
        }

        private void Update()
        {
            healthSystem.Tick();
        }
    }
}