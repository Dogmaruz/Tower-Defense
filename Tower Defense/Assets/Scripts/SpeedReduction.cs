using SpaceShooter;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense
{
    public class SpeedReduction : MonoBehaviour
    {
        [SerializeField] private float m_speedRatio = 0.5f;

        private void Awake()
        {
            var td_projectile = GetComponent<TD_Projectile>();

            td_projectile.EventOnHit.AddListener(ReduseSpeed);
        }

        public void ReduseSpeed(Enemy enemy)
        {
            enemy.GetComponent<TD_PatrolController>().SetNavigationLinear(m_speedRatio);
        }
    }
}