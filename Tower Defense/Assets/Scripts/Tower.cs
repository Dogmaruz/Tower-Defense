using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{

    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius { get => m_Radius; set { m_Radius = value; } }

        [SerializeField] private Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private Turret[] m_Turrets;

        private Destructible m_Target;

        private void Start()
        {
            m_Turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            if (m_Target)
            {
                Vector2 targetVector = m_Target.transform.position - transform.position;

                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (var turret in m_Turrets)
                    {
                        targetVector = m_Target.transform.position - turret.transform.position;
                        turret.transform.up = targetVector.normalized;
                        turret.Fire();
                    }
                } else
                {
                    m_Target = null;
                }

            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

                if (enter)
                {
                    m_Target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        /// <summary>
        /// Отрисовывает границы зоны патрулирования.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}