using UnityEngine;
using TowerDefense;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity; //Скорость.
        public float Velocity => m_Velocity;

        [SerializeField] private float m_LifeTime; //Время жизни.

        [SerializeField] protected int m_Damage; //Величина урона.

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private UnityEvent<Enemy> m_EventOnHit;
        public UnityEvent<Enemy> EventOnHit => m_EventOnHit;

#if UNITY_EDITOR
        //Метод для инспектора чтобы можно было передать параметры другому projectile.
        public void SetFromOtherProjectile(Projectile projectile)
        {
            projectile.GetData(out m_Velocity, out m_LifeTime, out m_Damage, out m_ImpactEffectPrefab);
        }

        private void GetData(out float m_Velocity, out float m_LifeTime, out int m_Damage, out ImpactEffect m_ImpactEffectPrefab)
        {
            m_Velocity = this.m_Velocity;

            m_LifeTime = this.m_LifeTime;

            m_Damage = this.m_Damage;

            m_ImpactEffectPrefab = this.m_ImpactEffectPrefab;
        }
#endif
        private void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;

            Vector2 step = transform.up * stepLenght;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            //Проверка на столкновение пули с объектом.

            if (hit)
            {
                OnHit(hit);

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_LifeTime)
            {
                m_EventOnDeath?.Invoke();

                Destroy(gameObject);
            }

            transform.position += new Vector3(step.x, step.y, 0);
        }

        //Старый код из Projectile.

        protected virtual void OnHit(RaycastHit2D hit)
        {
            Destructible destructible = hit.collider.transform.root.GetComponent<Destructible>();

            if (destructible != null && destructible != m_ParentDestructible)
            {
                destructible.ApplyDamage(m_Damage);

                //EventOnHit?.Invoke(destructible);

                AddingPoints(destructible);
            }
            else
            {
                destructible = hit.collider.GetComponentInParent<Destructible>();

                if (destructible != null && destructible != m_ParentDestructible)
                {
                    destructible.ApplyDamage(m_Damage);

                    //EventOnHit?.Invoke(destructible);

                    AddingPoints(destructible);
                }
            }
        }


        //Уничтожение с вызовом эфекта после попадания.
        private void OnProjectileLifeEnd(Collider2D collider, Vector2 pos)
        {
            if (m_ImpactEffectPrefab)
            {
                Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
            }

            m_EventOnDeath?.Invoke();

            Destroy(gameObject);
        }

        private Destructible m_ParentDestructible;
        public Destructible ParentDestructible { get => m_ParentDestructible; set => m_ParentDestructible = value; }

        //Задает родителя в классе Turret при выстреле.
        public void SetParentShooter(Destructible parentDestructible)
        {
            m_ParentDestructible = parentDestructible;

            //if (Player.Instance != null)
            //{
            //    if (m_ParentDestructible == Player.Instance.ActiveShip)
            //        m_IsPlayer = true;
            //    else
            //        m_IsPlayer = false;
            //}
        }

        //Добавляет очки и колличество уничтоженных объектов.
        public void AddingPoints(Destructible destructible)
        {
            //if (m_IsPlayer)
            //{
            if (destructible.CurrentHitPoint <= 0)
            {
                Player.Instance.AddKill();

                TD_Player.Instance.UpdateScore(destructible.KillValue);
            }
            else
                TD_Player.Instance.UpdateScore(destructible.ScoreValue);
            //}
        }
    }
}
#if UNITY_EDITOR
namespace TowerDefense
{
    [CustomEditor(typeof(SpaceShooter.Projectile))]
    public class ProjectileInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create TD_Projectile"))
            {
                var target = this.target as SpaceShooter.Projectile;

                var td_projectile = target.gameObject.AddComponent<TD_Projectile>();

                td_projectile.SetFromOtherProjectile(target);

                DestroyImmediate(target, true);
            }
        }
    }
}
#endif