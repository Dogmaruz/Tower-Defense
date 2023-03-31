using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class TD_Player : Player
    {

        [SerializeField] private int m_gold = 0;

        //Добавляет золото игроку.
        public void ChangeGold(int change)
        {
            m_gold += change;

        }
    }
}