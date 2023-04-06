using System;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То что может иметь хитпоинты.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Indestructibe;
        public bool IsIndestructibe { get => m_Indestructibe; set => m_Indestructibe = value; }

        /// <summary>
        /// Стартовое колличество хитпоинтов.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        public int HitPoints => m_HitPoints;

        /// <summary>
        /// Текущие хитпоинты.
        /// </summary>
        private int m_CurrentHitPoint;
        public int CurrentHitPoint => m_CurrentHitPoint;


        /// <summary>
        /// События уничтожения объекта.
        /// </summary>
        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        /// <summary>
        /// Событие обновления UI HP.
        /// </summary>
        [SerializeField] private UnityEvent<int> m_EventOnUpdateHP;
        public UnityEvent<int> EventOnUpdateHP => m_EventOnUpdateHP;

        //Ссылка на эффекты после уничтожения.
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
        /// Применение урона к объекту.
        /// </summary>
        /// <param name="damage">Урон наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructibe) return;

            m_CurrentHitPoint -= damage;

            EventOnUpdateHP?.Invoke(m_CurrentHitPoint);

            if (m_CurrentHitPoint <= 0) OnDeath();
        }

        #endregion

        /// <summary>
        /// Переопределяет событие уничтожения объекта, когда хитпоинты ниже или равны нулю.
        /// </summary>
        protected virtual void OnDeath()
        {
            //Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        #region Teams

        /// <summary>
        /// Статическое поле всех объектов типа Destructible.
        /// </summary>
        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructible => m_AllDestructibles;

        //Добавляет в коллекцию обект при создании.
        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
            {
                m_AllDestructibles  = new HashSet<Destructible>();
            }

            m_AllDestructibles.Add(this);
        }

        //Убирает из коллекции объект при уничтожении.
        protected virtual void OnDestroy()
        {
            m_AllDestructibles?.Remove(this);
        }

        protected void Use(EnemyAsset asset)
        {
            m_HitPoints = asset.HitPoints;

            m_ScoreValue = asset.ScoreValue;

            m_KillValue = asset.KillValue;
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        #endregion

        #region Score

        //Задает значение очков за попадание.
        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        //Задает значение очков за уничтожение.
        [SerializeField] private int m_KillValue;
        public int KillValue => m_KillValue;

        #endregion
    }
}


