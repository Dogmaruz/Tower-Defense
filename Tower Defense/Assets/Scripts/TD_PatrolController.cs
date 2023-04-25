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

               public void SetPath(Path newPath)
        {
            m_path = newPath;

            m_pathIndex = 0;

            SetPatrolBehaviour(m_path[m_pathIndex]);
        }

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

        public void SetNavigationLinear(float value)
        {
            m_NavigationLinear = value;
        }
    }
}