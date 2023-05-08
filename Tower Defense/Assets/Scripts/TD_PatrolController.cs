using SpaceShooter;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public class TD_PatrolController : AIController
    {
        private Path m_path;

        private int m_pathIndex;

        [SerializeField] private UnityEvent m_OnEndPath;

        public UnityEvent OnEndPath { get => m_OnEndPath; set => m_OnEndPath = value; }

        //Задает путь.
        public void SetPath(Path newPath)
        {
            m_path = newPath;

            m_pathIndex = 0;

            SetPatrolBehaviour(m_path[m_pathIndex]);
        }

        //Получает новую точку. Если достигнут конец пути вызываем событие.
        protected override void GetNewPoint()
        {
            m_pathIndex++;

            if (m_path.Lenght > m_pathIndex)
            {
                SetPatrolBehaviour(m_path[m_pathIndex]);
            }
            else
            {
                m_OnEndPath?.Invoke();

                Destroy(gameObject);
            }
        }

        //Задает линейную скорость. Используется для эффекта замедления врага или остановки при окончании уровня (победа или поражение).
        public void SetNavigationLinear(float value)
        {
            m_NavigationLinear = value;
        }
    }
}