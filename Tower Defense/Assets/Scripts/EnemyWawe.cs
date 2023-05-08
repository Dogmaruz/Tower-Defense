using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWawe : MonoBehaviour
    {
        public static Action<float, bool> OnWavePrepare;

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

        [SerializeField] private float m_PrepareTime = 10f;

        [SerializeField] private EnemyWawe m_NextWave;

        //Возвращает остаток времени.
        public float GetRemainingTime()
        {
            return m_PrepareTime - Time.time;
        }

        private event Action OnWaveReady;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time > m_PrepareTime)
            {
                OnWaveReady?.Invoke();

                enabled = false;
            }
        }

        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(m_PrepareTime, false);

            m_PrepareTime += Time.time; //Устанавливаем время волны от тещего временни.

            enabled = true;

            OnWaveReady += spawnEnemies;
        }

        //Подготавливает новую волну.
        public EnemyWawe PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;

            if (m_NextWave)
            {
                m_NextWave.Prepare(spawnEnemies);
            }
            else
            {
                OnWavePrepare?.Invoke(0, true);
            }

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