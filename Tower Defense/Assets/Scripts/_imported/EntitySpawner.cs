using UnityEngine;

namespace SpaceShooter
{
    public partial class EntitySpawner : MonoBehaviour
    {
        [SerializeField] private AIPointPatrol m_moveTarget;

        [SerializeField] private Entity[] m_EntityPrefabs;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private SpawnMode m_SpawnMode;

        [SerializeField] private int m_NumSpawns;

        [SerializeField] private float m_RespawnTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespawnTime;
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
                int index = Random.Range(0, m_EntityPrefabs.Length);

                GameObject newEntity = Instantiate(m_EntityPrefabs[index].gameObject);

                //newEntity.GetComponent<AIController>().PatrolPoints[0] = GetComponentInChildren<AIPointPatrol>();

                if (newEntity.TryGetComponent<AIController>(out var ai))
                {
                    ai.SetPatrolBehaviour(m_moveTarget);
                }

                newEntity.transform.position = m_Area.GetRandomInsideZone();
            }
        }
    }
}