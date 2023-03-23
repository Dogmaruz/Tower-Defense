using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// ����� ��� �������������� ��������� � ������.
        /// </summary>
        [Header("Space Ship")]
        [Space(6)]

        [SerializeField] private float m_Mass;

        /// <summary>
        /// ��������� ������ ����.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelosity;
        public float MaxLinearVelosity { get => m_MaxLinearVelosity; set => m_MaxLinearVelosity = value; }

        /// <summary>
        /// ������������ ������������ ��������. � ��������/���.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelosity;
        public float MaxAngularVelosity { get => m_MaxAngularVelosity; set => m_MaxAngularVelosity = value; }

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        /// <summary>
        /// ����������� �������� �������
        /// </summary>

        [Header("Ship Injectors")]
        [Space(6)]

        [SerializeField] private GameObject m_BackInjectors; //������ �� ������ ��������.

        [SerializeField] private GameObject m_ForwardInjectors; //������ �� �������� ��������.

        [SerializeField] private GameObject m_LeftInjectors; //������ �� ����� ��������.

        [SerializeField] private GameObject m_RightInjectors; //������ �� ������ ��������.

        /// <summary>
        /// ����������� ������ �� �����.
        /// </summary>
        private Rigidbody2D m_Rigidbody;

        #region Public API

        /// <summary>
        /// ���������� �������� �����. -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// ���������� ������������ ����� �����. -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        /// <summary>
        /// ���������� �����.
        /// </summary>
        public bool FireControl { get; set; }

        private float m_previousMaxLenearVelosity; //���������� �������� ��������.

        private float m_ratioAcceleration = 2f; //����������� ���������.

        #endregion

        #region Unity Event

        [SerializeField] private UnityEvent<float> m_EventOnUpdateEnergy;
        public UnityEvent<float> EventOnUpdateEnergy => m_EventOnUpdateEnergy;

        [SerializeField] private UnityEvent<float> m_EventOnUpdateAcceleration;
        public UnityEvent<float> EventOnUpdateAcceleration => m_EventOnUpdateAcceleration;

        [SerializeField] private UnityEvent<float> m_EventOnUpdateShield;
        public UnityEvent<float> EventOnUpdateShield => m_EventOnUpdateShield;

        private float m_Speed;
        public float Speed => m_Speed;

        private Vector3 m_oldPosition;

        protected override void Start()
        {
            base.Start();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Rigidbody.mass = m_Mass;

            m_Rigidbody.inertia = 1;

            //InitOffensive();

            m_oldPosition = transform.position;

            m_previousMaxLenearVelosity = MaxLinearVelosity;
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();

            PlayShipEffectInjectors();

            //UpdateEnergyRegeneration();

            //UpdateAcselerationRegeneration();

            //if (m_IsShildEmpty)
            //    UpdateShieldRegeneration();

            m_Speed = ((transform.position - m_oldPosition) / Time.deltaTime).magnitude;

            m_oldPosition = transform.position;
        }

        /// <summary>
        /// ����������� ������� �������� �������.
        /// </summary>
        private void PlayShipEffectInjectors()
        {
            if (ThrustControl < 0)
            {
                m_ForwardInjectors.SetActive(true);
                m_BackInjectors.SetActive(false);
            }

            if (ThrustControl > 0)
            {
                m_ForwardInjectors.SetActive(false);
                m_BackInjectors.SetActive(true);
            }

            if (ThrustControl == 0)
            {
                m_ForwardInjectors.SetActive(false);
                m_BackInjectors.SetActive(false);
            }

            if (TorqueControl > -0.5f && TorqueControl < 0.5f)
            {
                m_LeftInjectors.SetActive(false);
                m_RightInjectors.SetActive(false);
            }

            if (TorqueControl < -0.5f)
            {
                m_LeftInjectors.SetActive(true);
                m_RightInjectors.SetActive(false);
            }

            if (TorqueControl > 0.5f)
            {
                m_LeftInjectors.SetActive(false);
                m_RightInjectors.SetActive(true);
            }
        }

        #endregion

        /// <summary>
        /// ����� ���������� ��� ������� ��� �������� � ��������.
        /// </summary>
        private void UpdateRigidbody()
        {
            m_Rigidbody.AddForce(transform.up * m_Thrust * ThrustControl * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody.AddForce(-m_Rigidbody.velocity * m_Thrust / m_MaxLinearVelosity * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody.AddTorque(m_Mobility * TorqueControl * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * m_Mobility / m_MaxAngularVelosity * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        //TODO: �������� ��������� �����-��������. ������������ ��������.
        //������������� ��������.
        public bool DrawAmmo(int count)
        {
            
            return true;
        }

        //TODO: �������� ��������� �����-��������. ������������ ��������.
        //������������� ������� ��� �������� ��������� ������.
        public bool DrawEnergy(int count)
        {
            
            return true;
        }

        /*
        #region Offensive

        [SerializeField] private Turret[] m_Turrets;

        //���������� ������� � ������� ��������� ����.
        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy;
        public int MaxEnergy => m_MaxEnergy;

        [SerializeField] private int m_MaxAcceleration;
        public int MaxAcceleration => m_MaxAcceleration;

        [SerializeField] private int m_MaxShield;
        public int MaxShield => m_MaxShield;

        [SerializeField] private int m_MaxAmmo;

        [SerializeField] private float m_EnergyRegenerationPerSecond;

        [SerializeField] private float m_AccelerationRegenerationPerSecond;

        [SerializeField] private float m_ShieldRegenerationPerSecond;

        [SerializeField] private GameObject m_ShieldParticle;
        public GameObject ShieldParticle => m_ShieldParticle;

        private float m_PrimaryEnergy; //��������� ������.

        private int m_SecondaryAmmo; //��������� ������.
        public int SecondaryAmmo => m_SecondaryAmmo;

        private float m_Accelration;

        private float m_Shiled;

        private bool m_IsShildEmpty;
        public float Shield => m_Shiled;

        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);

            EventOnUpdateEnergy?.Invoke(m_PrimaryEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        //�������������� �������������.
        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = 0;
            m_Accelration = m_MaxAcceleration;
            m_Shiled = m_MaxShield;

            m_EventOnUpdateShield?.Invoke(m_Shiled);
        }
        //����������� �������.
        private void UpdateEnergyRegeneration()
        {
            m_PrimaryEnergy += m_EnergyRegenerationPerSecond * Time.deltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);

            EventOnUpdateEnergy?.Invoke(m_PrimaryEnergy);
        }

        //����������� ���������.
        private void UpdateAcselerationRegeneration()
        {
            m_Accelration += m_AccelerationRegenerationPerSecond * Time.deltaTime;
            m_Accelration = Mathf.Clamp(m_Accelration, 0, m_MaxAcceleration);

            EventOnUpdateAcceleration?.Invoke(m_Accelration);
        }

        //����������� ����.
        private void UpdateShieldRegeneration()
        {
            m_Shiled += m_ShieldRegenerationPerSecond * Time.deltaTime;
            m_Shiled = Mathf.Clamp(m_Shiled, 0, m_MaxShield);

            if (m_Shiled == m_MaxShield) m_IsShildEmpty = false;

            m_EventOnUpdateShield?.Invoke(m_Shiled);
        }

        

        //������������� ���������.
        public bool DrawAcceleration(float count)
        {
            m_Accelration -= count * Time.deltaTime;
            m_Accelration = Mathf.Clamp(m_Accelration, 0, m_MaxAcceleration);

            if (m_Accelration > 0)
            {
                return true;
            }
            else
                return false;
        }

        //������������� ����.
        public bool DrawShield(float count)
        {
            m_Shiled -= count * Time.deltaTime;
            m_Shiled = Mathf.Clamp(m_Shiled, 0, m_MaxShield);

            m_EventOnUpdateShield?.Invoke(m_Shiled);

            if (m_Shiled > 0)
            {
                return true;
            }

            m_IsShildEmpty = true;
            return false;
        }

        #endregion

        //���������� ���� ��������.
        public void AssingWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }

            AddAmmo(30);
        }
        */

        //������ �������� ���������.
        public void ShipAcseleration(bool isActive)
        {
            if (isActive)
            {
                MaxLinearVelosity = m_previousMaxLenearVelosity * m_ratioAcceleration;
            }
            else
            {
                MaxLinearVelosity = m_previousMaxLenearVelosity;
            }
        }
    }
}


