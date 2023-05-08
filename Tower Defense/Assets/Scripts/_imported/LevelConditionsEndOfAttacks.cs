using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class LevelConditionsEndOfAttacks : MonoBehaviour, ILevelCondition
    {
        private bool m_Reached; //���� �� ����������.

        //private bool IsSpawnwersEnded;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                m_Reached = true;
            };
        }

        public bool IsCompleted
        {
            get
            {
                #region ������ ��� ���������� ������ �� ������� - ���� ������ �����
                //var enemis = FindObjectsOfType<Enemy>();

                //var enemySpawners = FindObjectsOfType<EnemySpawner>();

                //foreach (var enemySpawner in enemySpawners)
                //{
                //    if (enemySpawner.IsCompleted) 
                //        IsSpawnwersEnded = true;
                //    else
                //    {
                //        IsSpawnwersEnded = false;

                //        break;
                //    }
                //}

                //if (enemis.Length == 0 && IsSpawnwersEnded)
                //{
                //    m_Reached = true;
                //}
                #endregion

                return m_Reached;
            }
        }
    }
}