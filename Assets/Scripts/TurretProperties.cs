using System;
using UnityEngine;


namespace LegendOfSlimeClone
{
    public enum TurretMode
    {
        Main,
        Secondary
    }
    [CreateAssetMenu]

    public sealed class TurretProperties : ScriptableObject
    {
        
        [SerializeField] private TurretMode m_TurretMode;
        public TurretMode TurretMode => m_TurretMode;

        [SerializeField] private Projectile m_projectilePrefab;
        public Projectile ProjectilePrefab => m_projectilePrefab;

        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;

        [SerializeField] private float m_MovementSpeed;
        public float MovementSpeed => m_MovementSpeed;

        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;

        [SerializeField] private int m_Damage;
        public int Damage => m_Damage;

        public void DecreseRefireTime(int amount)
        {
            if (m_RateOfFire <= 0.1f)
            {
                m_RateOfFire = 0.1f;
            } 
            else
            {
                m_RateOfFire -= m_RateOfFire * amount / 100;
            }
        }
        public void IncreaseDamage(int amount)
        {
            m_Damage += (int)(m_Damage * amount / 100);
        }

        public void SetDamage(int v)
        {
            m_Damage = v;
        }

        internal void SetRateOfFire(float v)
        {
           m_RateOfFire = v;
        }
    }
}

