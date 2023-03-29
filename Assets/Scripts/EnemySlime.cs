using UnityEngine;

namespace LegendOfSlimeClone
{


    public class EnemySlime : Destructible
    {
        [SerializeField] private int m_damage;
        [SerializeField] private float m_AttackSpeed;
        [SerializeField] private int m_GiveGold;

        private Timer m_AttackTimer = new Timer(0);

        private new void Start()
        {
            base.Start();
            m_AttackTimer.Start(m_AttackSpeed);
        }

        private void Update()
        {
            m_AttackTimer.RemoveTime(Time.deltaTime);
        }
        private void OnTriggerStay(Collider other)
        {
            Slime dest = other.transform.root.GetComponent<Slime>();
            if(dest)
            {
                if (m_AttackTimer.IsFinished)
                {
                    dest.ApplyDamage(m_damage);
                    m_AttackTimer.Start(m_AttackSpeed);
                }
            }
        }

        protected override void OnDeath()
        {
            Bag.Instance.ChangeGold(m_GiveGold);
            base.OnDeath();
        }
    }
}