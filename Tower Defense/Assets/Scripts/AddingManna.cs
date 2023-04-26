using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class AddingManna : MonoBehaviour
    {
        private void Awake()
        {
            var td_projectile = GetComponent<TD_Projectile>();

            td_projectile.EventOnHit.AddListener(Add);
        }

        public void Add(Enemy enemy)
        {
            if (enemy.GetComponent<Destructible>().CurrentHitPoint <= 0)
            {
                TD_Player.Instance.ChangeManna(1);
            }
        }
    }
}