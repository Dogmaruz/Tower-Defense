﻿using SpaceShooter;
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

        private static event Action<int> OnGoldUpdate;

        public static void GoldUpdateSubscrible(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.m_gold);
        }

        private static event Action<int> OnLifeUpdate;

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

        [SerializeField] private int m_gold = 0;


        //Добавляет золото игроку.
        public void ChangeGold(int change)
        {
            m_gold += change;

            OnGoldUpdate(m_gold);
        }

        public void UpdateScore(int value)
        {
            AddScore(value);

            OnScoreUpdate(Score);
        }

        //Добавляем урон игроку и обновляем жизни.
        public void ReduceLife(int damage)
        {
            TakeDamage(damage);

            OnLifeUpdate(m_NumLives);
        }

        [SerializeField] private Tower m_towerPrefabe;
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.GoldCost);

            var tower = Instantiate(m_towerPrefabe, buildSite.position, Quaternion.identity);

            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.ElementSprite;

            tower.GetComponentInChildren<Turret>().AssignTurretProperties(towerAsset.TurretProperties);

            if (towerAsset.EffectPrefab != null)
            {
                tower.IntatiateEffectPrefab(towerAsset.EffectPrefab);
            }

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
    }
}