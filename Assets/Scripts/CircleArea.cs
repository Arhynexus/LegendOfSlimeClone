using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CosmoSimClone
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius;

        public float Radius => m_Radius;

        public Vector3 GetRandomINsideZone()
        {
            var position = new Vector3(Random.Range(0, m_Radius), 0f, Random.Range(0, m_Radius));
            return position;
        }

#if UNITY_EDITOR
        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}
