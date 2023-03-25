using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private AIPointPatrol[] m_Points;

        [SerializeField] private float m_Radius;
        public float Radius { get => m_Radius; set { m_Radius = value; } }

        [SerializeField] private Color GizmoColor = new Color(1, 0, 0, 0.3f);
        public int Lenght { get => m_Points.Length; }

        public AIPointPatrol this[int i] { get => m_Points[i]; }

        private void OnDrawGizmos()
        {
            Gizmos.color = GizmoColor;

            for (int i = 0; i < m_Points.Length; i++)
            {
                Gizmos.DrawSphere(m_Points[i].transform.position, m_Radius);

                if (i > 0)
                {
                    Gizmos.DrawLine(m_Points[i].transform.position, m_Points[i - 1].transform.position);
                }
            }
        }
    }
}