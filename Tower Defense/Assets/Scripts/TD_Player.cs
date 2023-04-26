using SpaceShooter;
using System;
using UnityEngine;

namespace TowerDefense
{
    public class TD_Player : Player
    {
        public static new TD_Player Instance
        {
            get
            {
                return Player.Instance as TD_Player;
            }
        }


        [SerializeField] private UpgradeAsset m_heathUpgrade;

        private static event Action<int> OnGoldUpdate;

        private new void Awake()
        {
            base.Awake();

            var level = Upgrades.GetUpgradeLevel(m_heathUpgrade);

            TD_Player.Instance.m_NumLives += level;
        }

        public static void GoldUpdateSubscrible(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.m_gold);
        }

        public static event Action<int> OnLifeUpdate;

        public static void LifeUpdateSubscrible(Action<int> action)
        {
            OnLifeUpdate += action;
            action(Instance.m_NumLives);
        }

        private static event Action<int> OnScoreUpdate;

        public static void ScoreUpdateSubscrible(Action<int> action)
        {
            OnScoreUpdate += action;
            action(Instance.Score);
        }

        private static event Action<int> OnMannaUpdate;

        public static void MannaUpdateSubscrible(Action<int> action)
        {
            OnMannaUpdate += action;
            action(Instance.Manna);
        }

        [SerializeField] private int m_gold = 0;
        public int Gold { get => m_gold;}

        [SerializeField] private int m_Manna = 0;
        public int Manna { get => m_Manna; }


        //Добавляет золото игроку.
        public void ChangeGold(int change)
        {
            m_gold += change;

            OnGoldUpdate?.Invoke(m_gold);
        }

        public void ChangeManna(int change)
        {
            m_Manna += change;

            OnMannaUpdate?.Invoke(m_Manna);
        }

        public void UpdateScore(int value)
        {
            AddScore(value);

            OnScoreUpdate?.Invoke(Score);
        }

        //Добавляем урон игроку и обновляем жизни.
        public void ReduceLife(int damage)
        {
            TakeDamage(damage);

            OnLifeUpdate?.Invoke(m_NumLives);
        }

        [SerializeField] private Tower m_towerPrefabe;
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.GoldCost);

            var tower = Instantiate(m_towerPrefabe, buildSite.position, Quaternion.identity);

            tower.Use(towerAsset);

            Destroy(buildSite.gameObject);

        }

        public static void GoldUpdateUnSubscrible(Action<int> action)
        {
            OnGoldUpdate -= action;
        }


        public static void LifeUpdateUnSubscrible(Action<int> action)
        {
            OnLifeUpdate -= action;
        }

        public static void ScoreUpdateUnSubscrible(Action<int> action)
        {
            OnScoreUpdate -= action;
        }

        public static void MannaUpdateUnSubscrible(Action<int> action)
        {
            OnMannaUpdate -= action;
        }
    }
}