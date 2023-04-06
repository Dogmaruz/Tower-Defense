using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class EndOfAttacks : MonoBehaviour, ILevelCondition
    {
        private bool m_Reached; //Есть ли завершение.

        private bool IsSpawnwersEnded;

        public bool IsCompleted
        {
            get
            {
                var enemis = FindObjectsOfType<Enemy>();

                var enemySpawners = FindObjectsOfType<EnemySpawner>();

                foreach (var enemySpawner in enemySpawners)
                {
                    if (enemySpawner.IsCompleted) 
                        IsSpawnwersEnded = true;
                    else
                    {
                        IsSpawnwersEnded = false;
                        break;
                    }
                }

                if (enemis.Length == 0 && IsSpawnwersEnded)
                {
                    m_Reached = true;
                }

                return m_Reached;
            }
        }
    }
}