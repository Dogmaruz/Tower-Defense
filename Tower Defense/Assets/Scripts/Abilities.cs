using SpaceShooter;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{

    public class Abilities : SingletonBase<Abilities>
    {

        [Serializable]
        public class ExplosionAbility
        {//Способность наносить взрыв в радиусе.
            [SerializeField] private int m_Cost = 10; //Стоимость способности.

            [SerializeField] private Text m_CostText;

            [SerializeField] private int m_Damage = 2;

            [SerializeField] private float m_DamageRadius = 1;

            [SerializeField] private Color m_TargetColor;

            [SerializeField] private float m_CoolDown = 15; // Время до активации способности.

            public int Cost { get => m_Cost; set => m_Cost = value; }
            public int Damage { get => m_Damage; set => m_Damage = value; }
            public float DamageRadius { get => m_DamageRadius; set => m_DamageRadius = value; }
            public Text CostText { get => m_CostText; set => m_CostText = value; }

            public void Stop()
            {//Останавливаем корутины и делаем кнопку не активной.
                Instance.StopAllCoroutines();

                LevelResultController.Instance.OnShowPanel -= Stop;

                Instance.m_ExplosionButton.interactable = false;
            }

            public void Use()
            {
                IEnumerator ExplosionAbilityButton()
                {//Блокируем кнопку на время.
                    Instance.m_ExplosionButton.interactable = false;

                    Instance.IsCoolDownExplosion = true;

                    yield return new WaitForSeconds(m_CoolDown);

                    Instance.IsCoolDownExplosion = false;

                    Instance.UpdateExplosionAbility(Instance.Gold);
                }

                if (Instance.Gold >= m_Cost && Upgrades.GetUpgradeLevel(Instance.ExplosionUpgradeAsset) >= 1)
                {
                    TD_Player.Instance.ChangeGold(-m_Cost);

                    Instance.m_TargetCircle.enabled = true;

                    Instance.m_TargetCircle.color = m_TargetColor;

                    ClickProtection.Instance.Activate((Vector2 v) =>
                    {//Получаем позицию при клике мыши.
                        Vector3 position = v;

                        position.z = -Camera.main.transform.position.z;

                        position = Camera.main.ScreenToWorldPoint(position);

                        foreach (var collider in Physics2D.OverlapCircleAll(position, m_DamageRadius))
                        {//Наносим урон всем врагам в радиусе.
                            if (collider.transform.root.TryGetComponent<Enemy>(out var enemy))
                            {
                                enemy.TakeDamage(m_Damage, TD_Projectile.DamageType.Archer);
                            }
                        }
                    });

                    Instance.StartCoroutine(ExplosionAbilityButton());

                    Instance.UpdateExplosionAbility(Instance.Gold);
                }
            }
        }


        [Serializable]
        public class TimeAbility
        {//Способность замедлять врагов.
            [SerializeField] private int m_Cost = 10; //Стоимость способности.

            [SerializeField] private Text m_CostText;

            [SerializeField] private float m_CoolDown = 15; // Время до активации способности.

            [SerializeField] private float m_Duration = 5f; // Длиттельность эффекта.

            private float m_speedSlowRatio = 0.5f;

            public Text CostText { get => m_CostText; set => m_CostText = value; }
            public int Cost { get => m_Cost; set => m_Cost = value; }

            public void Use()
            {//Останавливаем корутины и делаем кнопку не активной.
                void Stop()
                {
                    Instance.StopAllCoroutines();

                    LevelResultController.Instance.OnShowPanel -= Stop;

                    EnemyWaveManager.OnEnemySpawn -= SlowEnemy;

                    Instance.m_TimeButton.interactable = false;
                }

                void Slow(TD_PatrolController patrolController, float speed)
                {//Замедляем врагов.
                    LevelResultController.Instance.OnShowPanel += Stop;

                    patrolController.SetNavigationLinear(speed);
                }

                void SlowEnemy(Enemy enemy)
                {
                    Slow(enemy.GetComponent<TD_PatrolController>(), m_speedSlowRatio);
                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);

                    foreach (var dest in Destructible.AllDestructible)
                    {//Восстанавливаем скорость в изначальное положение по истечении заданного времени.
                        Slow(dest.GetComponent<TD_PatrolController>(), dest.GetComponent<TD_PatrolController>().PreviousSpeed);
                    }

                    EnemyWaveManager.OnEnemySpawn -= SlowEnemy;
                }

                IEnumerator TimeAbilityButton()
                {//Блокируем кнопку на время.
                    Instance.m_TimeButton.interactable = false;

                    Instance.IsCoolDownTime = true;

                    yield return new WaitForSeconds(m_CoolDown);

                    Instance.IsCoolDownTime = false;

                    //Instance.m_TimeButton.interactable = true;

                    Instance.UpdateTimeAbility(Instance.Manna);

                    LevelResultController.Instance.OnShowPanel -= Stop;
                }

                if (Instance.Manna >= m_Cost && Upgrades.GetUpgradeLevel(Instance.TimeUpgradeAsset) >= 1)
                {//Уменьшаем манну и замедляем врагов.
                    TD_Player.Instance.ChangeManna(-m_Cost);

                    foreach (var dest in Destructible.AllDestructible)
                    {
                        Slow(dest.GetComponent<TD_PatrolController>(), m_speedSlowRatio);
                    }

                    EnemyWaveManager.OnEnemySpawn += SlowEnemy;

                    Instance.StartCoroutine(Restore());

                    Instance.StartCoroutine(TimeAbilityButton());

                    Instance.UpdateTimeAbility(Instance.Manna);
                }
            }
        }

        [SerializeField] private Button m_TimeButton;

        [SerializeField] private UpgradeAsset m_TimeUpgradeAsset;
        public UpgradeAsset TimeUpgradeAsset { get => m_TimeUpgradeAsset; }

        [SerializeField] private Button m_ExplosionButton;

        [SerializeField] private UpgradeAsset m_ExplosionUpgradeAsset;
        public UpgradeAsset ExplosionUpgradeAsset { get => m_ExplosionUpgradeAsset; }

        [SerializeField] private Image m_TargetCircle;
        public Image TargetCircle { get => m_TargetCircle; set => m_TargetCircle = value; }

        public int Gold { get; set; }
        public int Manna { get; set; }

        public bool IsCoolDownTime;

        public bool IsCoolDownExplosion;

        private void Start()
        {
            TD_Player.GoldUpdateSubscrible(UpdateExplosionAbility);

            TD_Player.MannaUpdateSubscrible(UpdateTimeAbility);

            LevelResultController.Instance.OnShowPanel += Instance.m_ExplosionAbility.Stop;

            if (Upgrades.GetUpgradeLevel(m_TimeUpgradeAsset) < 1 || Manna < Instance.m_TimeAbility.Cost)
            {
                m_TimeButton.interactable = false;
            }

            if (Upgrades.GetUpgradeLevel(m_ExplosionUpgradeAsset) < 1 || Gold < Instance.m_ExplosionAbility.Cost)
            {
                m_ExplosionButton.interactable = false;
            }
            else if (Upgrades.GetUpgradeLevel(m_ExplosionUpgradeAsset) > 1)
            {
                Instance.m_ExplosionAbility.Damage = 10;

                Instance.m_ExplosionAbility.DamageRadius = 1.5f;

                Instance.m_ExplosionAbility.Cost = 10;

                m_TargetCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            }

            //Debug.Log($"Damage : {Instance.m_ExplosionAbility.Damage} : Radius : {Instance.m_ExplosionAbility.DamageRadius} : Cost : {Instance.m_ExplosionAbility.Cost}");

            Instance.m_ExplosionAbility.CostText.text = Instance.m_ExplosionAbility.Cost.ToString();

            Instance.m_TimeAbility.CostText.text = Instance.m_TimeAbility.Cost.ToString();
        }

        private void UpdateExplosionAbility(int value)
        {//Активируем способность при достаточном колличестве монет.
            Gold = value;

            if (Gold >= Instance.m_ExplosionAbility.Cost != m_ExplosionButton.interactable && Upgrades.GetUpgradeLevel(Instance.ExplosionUpgradeAsset) >= 1 && !IsCoolDownExplosion)
            {
                m_ExplosionButton.interactable = !m_ExplosionButton.interactable;
            }
        }

        private void UpdateTimeAbility(int value)
        {//Активируем способность при достаточном колличестве манны.
            Manna = value;

            if (Manna >= Instance.m_TimeAbility.Cost != m_TimeButton.interactable && Upgrades.GetUpgradeLevel(Instance.TimeUpgradeAsset) >= 1 && !IsCoolDownTime)
            {
                m_TimeButton.interactable = !m_TimeButton.interactable;
            }
        }

        [SerializeField] private ExplosionAbility m_ExplosionAbility;
        public void UseExplosionAbility() => m_ExplosionAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;

        public void UseTimeAbility() => m_TimeAbility.Use();

        private void OnDestroy()
        {//Отписываемся от событий.
            TD_Player.GoldUpdateUnSubscrible(UpdateExplosionAbility);

            TD_Player.MannaUpdateUnSubscrible(UpdateTimeAbility);
        }
    }
}