using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius;

        [SerializeField] private Color m_Color;
        public float Radius => m_Radius;

        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2) Random.insideUnitSphere * m_Radius;
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Handles.color = m_Color;

            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
        #endif
    }
}