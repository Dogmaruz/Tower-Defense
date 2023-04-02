using SpaceShooter;
using UnityEngine;
using System;

namespace TowerDefense
{
    public class TD_Player : Player
    {
        public static new TD_Player Instance
        {
            get
            {
                return Player.Instance as TD_Player;
            }
        }

        public static event Action<int> OnGoldUpdate;
        public static event Action<int> OnLifeUpdate;

        [SerializeField] private int m_gold = 0;

        private void Start()
        {
            OnGoldUpdate(m_gold);
            OnLifeUpdate(m_NumLives);
        }

        //Добавляет золото игроку.
        public void ChangeGold(int change)
        {
            m_gold += change;

            OnGoldUpdate(m_gold);
        }

        public void ReduceLife(int damage)
        {
            TakeDamage(damage);

            OnLifeUpdate(m_NumLives);
        }
    }
}