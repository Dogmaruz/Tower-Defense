using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{

    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius { get => m_Radius; set { m_Radius = value; } }

        [SerializeField] Transform m_EffectSpawnPoint;

        [SerializeField] private UpgradeAsset m_radiusUpgrade;

        [SerializeField] private Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private Turret[] m_Turrets;

        private Destructible m_Target;

        private void Awake()
        {
            m_Turrets = GetComponentsInChildren<Turret>();

            var level = Upgrades.GetUpgradeLevel(m_radiusUpgrade);

            m_Radius += level * 0.2f;
        }

        //Задает параметры построенной башне.
        public void Use(TowerAsset towerAsset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.ElementSprite;

            foreach (var turret in m_Turrets)
            {
                turret.AssignTurretProperties(towerAsset.TurretProperties);
            }

            if (towerAsset.EffectPrefab != null)
            {
                IntatiateEffectPrefab(towerAsset.EffectPrefab);
            }

            GetComponentInChildren<BuildSite>().SetBuiddableTowers(towerAsset.UpgadesTo);
        }

        //Создает Particle System над башней. Этот эффект есть у башен второго уровня.
        public void IntatiateEffectPrefab(GameObject prefab)
        {
            Instantiate(prefab, m_EffectSpawnPoint.position, Quaternion.identity, m_EffectSpawnPoint);
        }

        private void Update()
        {
            if (m_Target)
            {//Если есть цель в радиусе зоны поражения башни, то производиться поворот турели и выстрел.
                Vector3 fromTo = m_Target.transform.position - transform.position;

                if (fromTo.magnitude <= m_Radius)
                {
                    foreach (var turret in m_Turrets)
                    {
                        Transform m_nearestEnemy = m_Target.transform;

                        var enemies = FindObjectsOfType<Enemy>();

                        float nearestEnemyDistance = Mathf.Infinity;

                        foreach (var enemy in enemies)
                        {
                            float dist = Vector2.Distance(transform.position, enemy.transform.position);

                            if (dist < nearestEnemyDistance)
                            {
                                m_nearestEnemy = enemy.transform;
                                nearestEnemyDistance = dist;
                            }
                        }

                        fromTo = m_nearestEnemy.transform.position - turret.transform.position;

                        //Мой вариант поворота балистики.
                        Vector3 fromToXY = new Vector3(fromTo.x, fromTo.y, 0f);

                        turret.transform.rotation = Quaternion.LookRotation(Vector3.forward, fromToXY);

                        float angle = Vector3.SignedAngle(Vector3.up, turret.transform.up, Vector3.forward);

                        if (angle > 90)
                        {
                            angle = 90 - (angle - 90);
                        }

                        if (angle < -90)
                        {
                            angle = -90 - (angle + 90);
                        }

                        turret.transform.eulerAngles = new Vector3(0f, 0f, angle);
                        //конец.

                        turret.Fire();
                    }
                }
                else
                {
                    m_Target = null;
                }

            }
            else
            {//Поиск цели.
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

                if (enter)
                {
                    m_Target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        /// <summary>
        /// Отрисовывает границы зоны патрулирования.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = GizmoColor;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}