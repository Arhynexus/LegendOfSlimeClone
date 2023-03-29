using System;
using UnityEngine;

namespace LegendOfSlimeClone
{


    public class Destructible : MonoBehaviour
    {
        [SerializeField] private int m_MaxHealth;
        public int MaxHealth => m_MaxHealth;

        public int CurrentHealth { get; protected set; }

        private event Action Death;
        public Action ChangeHitpoints;
        public Action<int> ReceivingDamage;

        protected void Start()
        {
            CurrentHealth = m_MaxHealth;
            Death += OnDeath;
        }
        public void ApplyDamage(int m_damage)
        {
            CurrentHealth -= m_damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Death();
            }
            ChangeHitpoints();
            ReceivingDamage(m_damage);
        }
        protected virtual void OnDeath()
        {
            Death -= OnDeath;
            Destroy(gameObject);
        }

        public void SetMaxHealth(int amount)
        {
            int healthBoost = m_MaxHealth + (int)(m_MaxHealth * amount / 100);
            m_MaxHealth = healthBoost;
            ChangeHitpoints();
        }
    }
}