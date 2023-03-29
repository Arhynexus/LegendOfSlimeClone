using System.Collections;
using UnityEngine;
using CosmoSimClone;

namespace LegendOfSlimeClone
{


    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private float m_JumpScale;
        [SerializeField] private float m_JumpTime;
        [SerializeField] private Slime m_TargetSlime;
        public void SetTargetSlime(Slime slime) => m_TargetSlime = slime;

        public Slime TargetSpaceShip => m_TargetSlime;

        [SerializeField] private Virtual_Joystick m_Virtual_Joystick;

        private float defaultSpeed;
        private float runSpeed;
        private float jumpSpeed;
        private float jumpSpeedWhileRunnig;

        private Collider m_PlayerCollider;
        private Rigidbody m_PlayerRigidbody;
        private Slime m_PlayerSlime;
        void Start()
        {
            SetStartStats();
            if (m_Virtual_Joystick)
            {
                m_Virtual_Joystick.gameObject.SetActive(true);
            }
            m_PlayerSlime = FindObjectOfType<Slime>();
        }

        private void SetStartStats()
        {
            defaultSpeed = m_Speed;
            runSpeed = m_RunSpeed + m_Speed;
            jumpSpeed = m_Speed * m_JumpScale;
            jumpSpeedWhileRunnig = jumpSpeed + runSpeed;

            m_PlayerRigidbody = GetComponent<Rigidbody>();
            m_PlayerCollider = m_PlayerRigidbody.GetComponentInChildren<Collider>();
            m_PlayerRigidbody.freezeRotation = true;
        }

        public void SpeedUp()
        {
            if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKeyDown(KeyCode.Space) == false)
            {
                m_Speed = runSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_Speed = defaultSpeed;
            }

        }

        private IEnumerator Jumping()
        {
            m_PlayerCollider.enabled = false;
            yield return new WaitForSeconds(m_JumpTime);
            m_Speed = defaultSpeed;
            m_PlayerCollider.enabled = true;
            yield return null;
        }

        private void JumpWhileRunnig()
        {
            if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey(KeyCode.Space) == true)
            {
                m_Speed = jumpSpeedWhileRunnig;
                StartCoroutine(Jumping());
            }
        }
        public void Jump()
        {
            if (Input.GetKey(KeyCode.LeftShift) == false && Input.GetKeyDown(KeyCode.Space) == true)
            {
                m_Speed = jumpSpeed;
                StartCoroutine(Jumping());
            }
        }


        void Update()
        {
            if (m_PlayerSlime.CurrentHealth > 0)
            {
                float horizontal = 0;
                float vertical = 0;

                if (m_Virtual_Joystick)
                {
                    Vector3 dir = m_Virtual_Joystick.value;
                    horizontal = Vector2.Dot(dir, m_TargetSlime.transform.right);
                    vertical = Vector2.Dot(dir, m_TargetSlime.transform.up);

                }
                if (m_Virtual_Joystick == null)
                {
                    horizontal = Input.GetAxis("Horizontal");
                    vertical = Input.GetAxis("Vertical");
                }

                SpeedUp();
                Jump();
                JumpWhileRunnig();

                m_PlayerRigidbody.velocity = new Vector3(Mathf.Abs(horizontal) * m_Speed, 0, vertical * m_Speed);
                m_PlayerRigidbody.angularVelocity = new Vector3(0, 0, 0);
            }
        }
    }
}