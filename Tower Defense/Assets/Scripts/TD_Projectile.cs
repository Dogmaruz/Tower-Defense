using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class TD_Projectile : Projectile
    {
        public enum DamageType
        {
            Archer,
            Magic,
            Stone
        }

        [SerializeField] private DamageType m_DamageType;

        //Проверяет попадание projectile с врагом и вызывает метод TakeDamage.
        protected override void OnHit(RaycastHit2D hit)
        {
            Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage, m_DamageType);

                EventOnHit?.Invoke(enemy);
            }
            else
            {
                enemy = hit.collider.GetComponentInParent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(m_Damage, m_DamageType);

                    EventOnHit?.Invoke(enemy);
                }
            }
        }
    }
}