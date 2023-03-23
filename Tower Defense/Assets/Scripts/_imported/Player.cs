using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_MaxLives; //������������ ����������� ������.
        private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship; //������ �� ������� ������.
        [SerializeField] private GameObject m_PlayerShipPrefab; //������� ������� ������.

        public SpaceShip ActiveShip => m_Ship; //������ �� ������� ������.

        //[SerializeField] private CameraController m_CameraController;
        //[SerializeField] private MovementController m_MovementController;
        //[SerializeField] private LivesUI m_LivesUI; //������ �� UI ����������� ����������� ������.

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
        //[SerializeField] private UIEnergyProgress m_UIEnergyProgress; //UI ������ �������.
        //[SerializeField] private UIAccelerationProgress m_UIAccelerationProgress; //UI ������ ���������.
        //[SerializeField] private UIShieldProgress m_UIShieldProgress;//UI ������ ����.

        private void Start()
        {
            Respawn();
        }

        //���������� ��� ����������� ������� ������.
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
        /// ����������� ������ ������.
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

                m_Ship.EventOnDeath?.AddListener(OnShipDeath);

                //m_CameraController.SetTarget(m_Ship.transform);

                //m_MovementController.SetTargetShip(m_Ship);
            }
        }

        #region Score

        public int Score { get; private set; } //����.
        public int NumKills { get; private set; } //����������� ������������ ��������.

        //��������� ����������� ������������ ��������.
        public void AddKill()
        {
            NumKills++;
        }

        //����������� ����.
        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}