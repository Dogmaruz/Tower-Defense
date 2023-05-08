using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class LevelConditionsTime : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float m_timeLimit = 10f;

        private bool m_Reached; //Есть ли завершение.

        public bool IsCompleted
        { 
            get
            {
                if (LevelController.Instance.LevelTime > m_timeLimit)
                {//Проверяет лимит времени прохождения уровня.
                    m_Reached = true;
                }

                return m_Reached;
            }
        }
    }
}