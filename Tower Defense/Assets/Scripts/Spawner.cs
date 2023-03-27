using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    public abstract class Spawner : MonoBehaviour
    {
        protected abstract GameObject GenerateSpawnedEntity();

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private SpawnMode m_SpawnMode;

        [SerializeField] private int m_NumSpawns;

        [SerializeField] private float m_RespawnTime;

        [SerializeField] private float m_SpawnDalay;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_SpawnDalay;
        }

        private void Update()
        {
            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        //Спавн объектов.
        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                var newEntity = GenerateSpawnedEntity();

                newEntity.transform.position = m_Area.GetRandomInsideZone();
            }
        }
    }
}