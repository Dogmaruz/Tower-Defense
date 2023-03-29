using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy m_EnemyPrefabs; //Ссылка на префаб.

        [SerializeField] private EnemyAsset[] m_EnemyAsset; //Настройки врага.

        [SerializeField] private Path m_Path; //Ссылка на путь.

        protected override GameObject GenerateSpawnedEntity()
        {
            var  newEnemy = Instantiate(m_EnemyPrefabs);

            newEnemy.Use(m_EnemyAsset[Random.Range(0, m_EnemyAsset.Length)]);

            newEnemy.GetComponent<TD_PatrolController>().SetPath(m_Path);

            return newEnemy.gameObject;
        }
    }
}