using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class TD_PatrolController : AIController
    {
        private Path m_path;
        private int m_pathIndex;
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
            } else
            {
                Destroy(gameObject);
            }
        }
    }
}