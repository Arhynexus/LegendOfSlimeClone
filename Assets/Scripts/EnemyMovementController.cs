using LegendOfSlimeClone;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    private Rigidbody m_EnemyRigidbody;

    private Slime m_Player;
    private float m_PlayerOffset = 1f;

    void Start()
    {
        m_Player = FindObjectOfType<Slime>();
        m_EnemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(m_Player)
        {
            float collisionPosition = Vector3.Distance(transform.position, m_Player.transform.position);
            if (collisionPosition > m_PlayerOffset)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_Player.transform.position, m_Speed * Time.deltaTime);
            }
            else m_EnemyRigidbody.velocity = new Vector3(0, 0, 0);
        }
    }
}
