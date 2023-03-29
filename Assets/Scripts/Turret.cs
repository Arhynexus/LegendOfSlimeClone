using UnityEngine;

namespace LegendOfSlimeClone
{
    public class Turret : MonoBehaviour
    {

        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private Slime m_Slime;

        [SerializeField] private AudioSource m_AudioSource;


        void Start()
        {
            m_Slime = transform.root.GetComponent<Slime>();
        }

        


        void Update()
        {
            if(m_RefireTimer > 0) m_RefireTimer -= Time.deltaTime;
            if (Input.GetMouseButton(0) == true)
            {
                Fire();
            }
            if(Input.GetKeyDown(KeyCode.F)) 
            {
                m_TurretProperties.DecreseRefireTime(50);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_TurretProperties.IncreaseDamage(50);
            }
        }

        // Public API

        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;

            if (m_AudioSource)
            {
                m_AudioSource.Play();
            }
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectile.SetVelocity(m_TurretProperties.MovementSpeed);
            projectile.SetDamage(m_TurretProperties.Damage);
            projectile.SetParentShooter(m_Slime);

            // Add Consumption of Ammo and Energy

            m_RefireTimer = m_TurretProperties.RateOfFire;

            // Add play sound for turret
        }

        public void AssignLoadOut(TurretProperties props)
        {
            if(m_Mode != props.TurretMode) return;
            m_RefireTimer = 0;
            m_TurretProperties = props;
        }
    }
}


