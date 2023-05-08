using UnityEngine;

namespace TowerDefense
{
    public class SpeedReduction : MonoBehaviour
    {
        [SerializeField] private float m_speedRatio = 0.5f;

        private void Awake()
        {
            var td_projectile = GetComponent<TD_Projectile>();

            td_projectile.EventOnHit.AddListener(ReduceSpeed);
        }

        //Уменьшает скорость врага в два раза.
        public void ReduceSpeed(Enemy enemy)
        {
            enemy.GetComponent<TD_PatrolController>().SetNavigationLinear(m_speedRatio);
        }
    }
}