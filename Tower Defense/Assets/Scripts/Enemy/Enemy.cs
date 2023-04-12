using SpaceShooter;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [RequireComponent(typeof(TD_PatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 1;

        [SerializeField] private int m_Gold = 1;
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