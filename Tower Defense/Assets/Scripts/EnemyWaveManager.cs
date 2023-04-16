using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy m_EnemyPrefabs; //—сылка на префаб.

        [SerializeField] private Path[] m_paths;

        [SerializeField] private EnemyWawe m_currentWave;

        [SerializeField] private int m_activeEnemyCount = 0;

        public event Action OnAllWavesDead;

        private void RecordEnemyDead()
        {
            if (--m_activeEnemyCount <= 0)
            {
                ForceNextWave();
            }
        }

        private void Start()
        {
            m_currentWave.Prepare(SpawnEnemies);
        }

        public void ForceNextWave()
        {
            if (m_currentWave)
            {
                TD_Player.Instance.ChangeGold((int)m_currentWave.GetRemainingTime());

                SpawnEnemies();
            }
            else
            {
                if (m_activeEnemyCount == 0)
                {
                    OnAllWavesDead?.Invoke();
                }
            }
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in m_currentWave.EnumerateSquad())
            {
                if (pathIndex < m_paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var newEnemy = Instantiate(m_EnemyPrefabs, m_paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);

                        newEnemy.OnDead += RecordEnemyDead;

                        newEnemy.Use(asset);

                        newEnemy.GetComponent<TD_PatrolController>().SetPath(m_paths[pathIndex]);

                        m_activeEnemyCount++;
                    }
                }
                else
                {
                    Debug.Log($"Invalid pathIndex in {name}");
                }

            }

            m_currentWave = m_currentWave.PrepareNext(SpawnEnemies);
        }
    }
}