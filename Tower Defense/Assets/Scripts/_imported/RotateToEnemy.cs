using TowerDefense;
using UnityEngine;

namespace SpaceShooter
{
    public class RotateToEnemy : MonoBehaviour
    {
        [SerializeField] protected float m_RorationSpeed;

        protected Transform m_nearestEnemy;

        void Start()
        {
            var enemies = FindObjectsOfType<Enemy>();

            float nearestEnemyDistance = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);

                if (dist < nearestEnemyDistance)
                {
                    m_nearestEnemy = enemy.transform;

                    nearestEnemyDistance = dist;
                }
            }

            if (m_nearestEnemy != null)
            {
                enabled = true;
            }
            else
                enabled = false;
        }


        void Update()
        {
                if (m_nearestEnemy == null) return;

                Vector3 fromTo = m_nearestEnemy.position - transform.position;

                Vector3 fromToXY = new Vector3(fromTo.x, fromTo.y, 0f);

                transform.up = Vector3.Slerp(transform.up, fromToXY, Time.deltaTime * m_RorationSpeed);
        }
    }
}