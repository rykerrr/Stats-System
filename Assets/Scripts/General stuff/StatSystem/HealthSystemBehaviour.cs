using System;
using UnityEngine;

namespace StatSystem.TakeOne
{
    public class HealthSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehaviour = default;
        [SerializeField] private HealthSystem healthSystem = default;

        public HealthSystem HealthSystem => healthSystem;

        private void Start()
        {
            healthSystem.Init(statsSysBehaviour.StatsSystem);

            healthSystem.onDeathEvent += (g) => gameObject.SetActive(false);
        }

        private void Update()
        {
            HealthSystem.Tick();
        }
        
        #region debug
        [ContextMenu("Dump health system data")]
        public void DumpHealthSystemData()
        {
            Debug.Log(HealthSystem.ToString());
        }
        #endregion
    }
}