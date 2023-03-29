using System;
using UnityEngine;

namespace LegendOfSlimeClone
{
    public class Slime : Destructible
    {
        
        [SerializeField] protected int m_HealthRegenAmount = 1;
        public int HealthRegenAmount => m_HealthRegenAmount;

        public Action<int> ReceivingRegen;
        private new void Start()
        {
            base.Start();
            m_HealthRegenTimer.Start(m_HealthTimeRegen);
        }


        Timer m_HealthRegenTimer = new Timer(0);
        private float m_HealthTimeRegen = 1f;
        public void HealthRegen(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            ReceivingRegen(amount);
        }

        public void IncreaseHealthRegen()
        {
            m_HealthRegenAmount += 1;
        }

        private void Update()
        {
            m_HealthRegenTimer.RemoveTime(Time.deltaTime);

            if (m_HealthRegenTimer.IsFinished)
            {
                m_HealthRegenTimer.Start(m_HealthTimeRegen);
                HealthRegen(m_HealthRegenAmount);
                ChangeHitpoints();
            }
        }

        public void SetHealthRegen(int v)
        {
            m_HealthRegenAmount= v;
        }

        protected override void OnDeath()
        {
            ResultPanelController.Instance.ShowResults(false);
            base.OnDeath();
        }
    }
}