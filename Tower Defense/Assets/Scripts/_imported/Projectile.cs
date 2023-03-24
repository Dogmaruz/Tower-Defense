using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity; //Скорость.

        [SerializeField] private float m_LifeTime; //Время жизни.

        [SerializeField] private int m_Damage; //Величина урона.

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        private bool m_IsPlayer;

        private void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            //Проверка на столкновение пули с объектом.
            if (hit)
            {
                Destructible destructible = hit.collider.transform.root.GetComponent<Destructible>();

                if (destructible != null && destructible != m_ParentDestructible)
                {
                    destructible.ApplyDamage(m_Damage);

                    AddingPoints(destructible);
                }
                else
                {
                    destructible = hit.collider.GetComponentInParent<Destructible>();

                    if (destructible != null && destructible != m_ParentDestructible)
                    {
                        destructible.ApplyDamage(m_Damage);

                        AddingPoints(destructible);
                    }
                }

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

        //Уничтожение с вызовом эфекта после попадания.
        private void OnProjectileLifeEnd(Collider2D collider, Vector2 pos)
        {
            //Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);

            m_EventOnDeath?.Invoke();

            Destroy(gameObject);
        }

        private Destructible m_ParentDestructible;
        public Destructible ParentDestructible { get => m_ParentDestructible; set => m_ParentDestructible = value; }

        //Задает родителя в классе Turret при выстреле.
        public void SetParentShooter(Destructible parentDestructible)
        {
            m_ParentDestructible = parentDestructible;

            if (Player.Instance != null)
            {
                if (m_ParentDestructible == Player.Instance.ActiveShip)
                    m_IsPlayer = true;
                else
                    m_IsPlayer = false;
            }
        }

        //Добавляет очки и колличество уничтоженных объектов.
        public void AddingPoints(Destructible destructible)
        {
            if (m_IsPlayer)
            {
                if (destructible.CurrentHitPoint <= 0)
                {
                    Player.Instance.AddKill();
                    Player.Instance.AddScore(destructible.KillValue);
                }
                else
                    Player.Instance.AddScore(destructible.ScoreValue);
            }
        }
    }
}