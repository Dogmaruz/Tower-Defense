using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ ������ �� �����. �� ��� ����� ����� ���������.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// ������ ���������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestructibe;
        public bool IsIndestructibe { get => m_Indestructibe; set => m_Indestructibe = value; }

        /// <summary>
        /// ��������� ����������� ����������.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        public int HitPoints => m_HitPoints;

        /// <summary>
        /// ������� ���������.
        /// </summary>
        private int m_CurrentHitPoint;
        public int CurrentHitPoint => m_CurrentHitPoint;


        /// <summary>
        /// ������� ����������� �������.
        /// </summary>
        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        /// <summary>
        /// ������� ���������� UI HP.
        /// </summary>
        [SerializeField] private UnityEvent<int> m_EventOnUpdateHP;
        public UnityEvent<int> EventOnUpdateHP => m_EventOnUpdateHP;

        //������ �� ������� ����� �����������.
        [SerializeField] private GameObject m_ImpactEffect;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoint = m_HitPoints;

            EventOnUpdateHP?.Invoke(m_CurrentHitPoint);
        }

        #endregion

        #region Public API

        /// <summary>
        /// ���������� ����� � �������.
        /// </summary>
        /// <param name="damage">���� ��������� �������</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructibe) return;

            m_CurrentHitPoint -= damage;

            EventOnUpdateHP?.Invoke(m_CurrentHitPoint);

            if (m_CurrentHitPoint <= 0) OnDeath();
        }

        #endregion

        /// <summary>
        /// �������������� ������� ����������� �������, ����� ��������� ���� ��� ����� ����.
        /// </summary>
        protected virtual void OnDeath()
        {
            Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        #region Teams

        /// <summary>
        /// ����������� ���� ���� �������� ���� Destructible.
        /// </summary>
        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructible => m_AllDestructibles;

        //��������� � ��������� ����� ��� ��������.
        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
            {
                m_AllDestructibles  = new HashSet<Destructible>();
            }

            m_AllDestructibles.Add(this);
        }

        //������� �� ��������� ������ ��� �����������.
        protected virtual void OnDestroy()
        {
            m_AllDestructibles?.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        #endregion

        #region Score

        //������ �������� ����� �� ���������.
        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        //������ �������� ����� �� �����������.
        [SerializeField] private int m_KillValue;
        public int KillValue => m_KillValue;

        #endregion
    }
}


