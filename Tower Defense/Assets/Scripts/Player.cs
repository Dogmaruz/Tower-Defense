using System;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] protected int m_MaxLives; //Максимальное колличество жизней.

        protected int m_NumLives;

        public event Action OnPlayerDead;

        [SerializeField] private SpaceShip m_Ship; //Ссылка на корабль игрока.

        [SerializeField] private GameObject m_PlayerShipPrefab; //Префабе корабля игрока.

        public SpaceShip ActiveShip => m_Ship; //Ссылка на корабль игрока.

        //[SerializeField] private CameraController m_CameraController;

        //[SerializeField] private MovementController m_MovementController;

        //[SerializeField] private LivesUI m_LivesUI; //Ссылка на UI отображения колличества жизней.

        protected override void Awake()
        {
            base.Awake();

            m_NumLives = m_MaxLives;

            //m_LivesUI.Setup(m_MaxLives);

            //m_LivesUI.UpdateLivesUI(m_NumLives);

            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        //[SerializeField] private Text m_TextGameOver;

        //[SerializeField] private UIEnergyProgress m_UIEnergyProgress; //UI уровня энергии.

        //[SerializeField] private UIAccelerationProgress m_UIAccelerationProgress; //UI уровня ускорения.

        //[SerializeField] private UIShieldProgress m_UIShieldProgress;//UI уровня щита.

        private void Start()
        {
            Respawn();
        }

        //Вызывается при уничтожении корабля игрока.
        private void OnShipDeath()
        {
            m_NumLives--;

            LevelSequenceController.Instance.EpisodeStatistics.NumberOfDeaths++;

            //m_LivesUI.UpdateLivesUI(m_NumLives);

            if (m_NumLives > 0)
            {
                Invoke(nameof(Respawn), 1f);
            }
            else
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }

        /// <summary>
        /// Перерождает кораль игрока.
        /// </summary>
        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                //m_UIEnergyProgress.UpdateShip(m_Ship);

                //m_UIAccelerationProgress.UpdateShip(m_Ship);

                //m_UIShieldProgress.UpdateShip(m_Ship);

                if (m_Ship)
                {
                    m_Ship.EventOnDeath?.AddListener(OnShipDeath);
                }

                //m_CameraController.SetTarget(m_Ship.transform);

                //m_MovementController.SetTargetShip(m_Ship);
            }
        }

        #region Score

        public int Score { get; private set; } //Счет.
        public int NumKills { get; private set; } //Колличество уничтоженных объектов.

        //Добавляет колличество уничтоженных объектов.
        public void AddKill()
        {
            NumKills++;
        }

        //Увеличивает счет.
        public void AddScore(int num)
        {
            Score += num;
        }

        //Наносит урон игроку и если жизни меньше или раны нулю перезапуск уровня.
        protected void TakeDamage(int damage)
        {
            m_NumLives -= damage;

            if (m_NumLives <= 0)
            {
                m_NumLives = 0;

                OnPlayerDead?.Invoke();

                //LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }

        #endregion

        private void OnDestroy()
        {
            OnPlayerDead = null;
        }
    }
}