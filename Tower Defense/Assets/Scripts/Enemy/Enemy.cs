using SpaceShooter;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TD_PatrolController))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType
        {
            Base = 0,
            Magic = 1,
            Iron = 2
        }

        private static Func<int, TD_Projectile.DamageType, int, int>[] ArmorDamageFunction =
        {
            (int damage, TD_Projectile.DamageType type, int armor) =>
            { //ArmorType.Base

                switch (type)
                {
                    case TD_Projectile.DamageType.Magic:
                        return Mathf.Max(damage - armor / 2, 1);

                    case TD_Projectile.DamageType.Stone:
                        return damage;

                    case TD_Projectile.DamageType.Archer:
                        return Mathf.Max(damage - armor, 1);

                    default:
                        return Mathf.Max(damage - armor, 1);
                }
            },

            (int damage, TD_Projectile.DamageType type, int armor) =>
            { //ArmorType.Magic

                switch (type)
                {
                    case TD_Projectile.DamageType.Archer:
                        return Mathf.Max(damage - armor / 2, 1);

                    case TD_Projectile.DamageType.Magic:
                        return Mathf.Max(damage - armor, 1);

                    case TD_Projectile.DamageType.Stone:
                        return damage;

                    default:
                        return Mathf.Max(damage - armor, 1);
                }
            },

            (int damage, TD_Projectile.DamageType type, int armor) =>
            { //ArmorType.Iron

                switch (type)
                {
                    case TD_Projectile.DamageType.Archer:
                        return Mathf.Max(damage - armor, 1);

                    case TD_Projectile.DamageType.Magic:
                        return 1;

                    case TD_Projectile.DamageType.Stone:
                        return Mathf.Max(damage - armor / 2, 1);

                    default:
                        return 1;
                }
            }
        };

        [SerializeField] private int m_Damage = 1;

        [SerializeField] private int m_Gold = 1;

        [SerializeField] private int m_armor = 0;

        [SerializeField] private ArmorType m_armorType;

        public event Action OnDead;

        private Destructible m_Destructible;

        private void Awake()
        {
            m_Destructible = GetComponent<Destructible>();

            m_Destructible.EventOnDeath.AddListener(Dead);

            GetComponent<TD_PatrolController>().OnEndPath.AddListener(Dead);
        }

        public void Dead()
        {
            OnDead?.Invoke();
        }

        private void OnDestroy()
        {
            m_Destructible.EventOnDeath.RemoveListener(Dead);

            OnDead = null;
        }

        public void Use(EnemyAsset asset)
        {

            var sr = GetComponentInChildren<SpriteRenderer>();

            sr.color = asset.color;

            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1f);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.controller;

            GetComponent<SpaceShip>().Use(asset);

            var collider = GetComponentInChildren<CircleCollider2D>();

            collider.radius = asset.ColliderRadius;

            collider.bounds.center.Set(0, 0, 0);

            m_Damage = asset.Damage;

            m_armor = asset.Armor;

            m_armorType = asset.ArmorType;

            m_Gold = asset.Gold;

        }

        public void GivePlayerGold()
        {
            TD_Player.Instance.ChangeGold(m_Gold);
        }

        public void DamagePlayer()
        {
            TD_Player.Instance.ReduceLife(m_Damage);
        }

        public void TakeDamage(int damage, TD_Projectile.DamageType damageType)
        {
            m_Destructible.ApplyDamage(ArmorDamageFunction[(int)m_armorType](damage, damageType, m_armor));
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset asset = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (asset)
            {
                (target as Enemy).Use(asset);
            }
        }
    }
#endif
}