using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWawe : MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField] private PathGroup[] groups;

        [SerializeField] private float prepareTime = 10f;

        [SerializeField] private EnemyWawe m_NextWave;

        private event Action OnWaveReady;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time > prepareTime)
            {
                enabled = false;

                OnWaveReady?.Invoke();
            }
        }
        public void Prepare(Action spawnEnemies)
        {
            prepareTime += Time.time;

            enabled = true;

            OnWaveReady += spawnEnemies;
        }

        public EnemyWawe PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;

            if (m_NextWave) m_NextWave.Prepare(spawnEnemies);

            return m_NextWave;
        }
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquad()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                }
            }

        }
    }
}