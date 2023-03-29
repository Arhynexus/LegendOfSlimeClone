using UnityEngine;

namespace LegendOfSlimeClone
{


    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform m_PlayerTransform;
        [SerializeField] private float m_TrackingSpeed = 1.5f;
        [SerializeField] private float m_XOffset;
        [SerializeField] private float m_ZOffset;
        private Vector3 target;

        private void FixedUpdate()
        {
            if (m_PlayerTransform)
            {
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, m_TrackingSpeed * Time.deltaTime);
                target = new Vector3(m_PlayerTransform.transform.position.x + m_XOffset, transform.position.y, m_PlayerTransform.transform.position.z - m_ZOffset);
                transform.position = currentPosition;
            }
        }
    }
}