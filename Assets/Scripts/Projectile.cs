using UnityEngine;

namespace LegendOfSlimeClone
{
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// Скорость движения снаряда
        /// </summary>
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;
        /// <summary>
        /// Время жизни снаряда
        /// </summary>
        [SerializeField] private float lifeTime;
        /// <summary>
        /// Урон, наносимый снарядом
        /// </summary>
        [SerializeField] private int m_Damage;
        /// <summary>
        /// Эффект столкновения
        /// </summary>
        private float m_Timer;

        private Transform m_Target;


        protected virtual void Start()
        {
            Destroy(gameObject, lifeTime);
            SearchTarget();
        }

        protected virtual void FixedUpdate()
        {
            if (m_Target != null)
            {
                if (Vector2.Distance(transform.position, m_Target.position) < 0.1f)
                {
                    m_Target.transform.root.GetComponent<Destructible>().ApplyDamage(m_Damage);
                    Destroy(gameObject);
                }
                else
                {
                    Vector3 dir = m_Target.position - transform.position;             // движение по 3 осям.
                    transform.position += (dir.normalized * Time.deltaTime * m_Velocity);
                    transform.up = dir.normalized;
                }
            }
            else
            {
                float stepLength = Time.deltaTime * m_Velocity;
                Vector2 step = m_Parent.transform.right * stepLength;

                transform.position += new Vector3(step.x, 0, step.y);
                SearchTarget();
            }
        }
        /*
        protected virtual void OnTriggerEnter(Collider collision)
        {
            var dest = collision.transform.root.GetComponentInParent<Destructible>();

            if (dest != null && dest != m_Parent)
            {
                dest.ApplyDamage(m_Damage);
                Destroy(gameObject);
            }
        }
        */
        public void SetVelocity(float velocity)
        {
            m_Velocity = velocity;
        }

        /// <summary>
        /// Метод, вызываемый при окончании жизни объекта
        /// </summary>
        protected virtual void OnProjectileLifeEnd(Collider2D collider, Vector2 point)
        {
            /*
            if (m_ImpactEffectPrefab_01 != null)
            {
                Instantiate(m_ImpactEffectPrefab_01, transform.position, Quaternion.identity);
            }
            if (m_ImpactEffectPrefab_02 != null)
            {
                Instantiate(m_ImpactEffectPrefab_02, transform.position, Quaternion.identity);
            }
            */
            Destroy(gameObject);
        }

        private Destructible m_Parent;

        /// <summary>
        /// Назначаем родителя выпущенному снаряду
        /// </summary>
        /// <param name="parent">Родитель выпущенного снаряда</param>
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        public void SetDamage(int damage)
        {
            m_Damage = damage;
        }
        public void SearchTarget()   // поиск ближайшего врага   и стрельба по нему ракетой.
        {
            Transform nearestEnemy = null;
            float nearestEnemyDistance = Mathf.Infinity;

            foreach (var enemy in FindObjectsOfType<EnemySlime>())
            {
                float currdistance = Vector2.Distance(transform.position, enemy.transform.position);
                if (currdistance < nearestEnemyDistance)
                {
                    nearestEnemy = enemy.transform;
                    nearestEnemyDistance = currdistance;
                }
            }
            if (nearestEnemy != null)
            {
                SetTarget(nearestEnemy.transform);
            }
        }


        public void SetTarget(Transform enemy)
        {
            m_Target = enemy;
        }
    }
}
