using SpaceShooter;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace TowerDefense
{
    [RequireComponent(typeof(TD_PatrolController))]
    public class Enemy : MonoBehaviour
    {
        public void Use(EnemyAsset asset)
        {

            var sr = GetComponentInChildren<SpriteRenderer>();

            sr.color = asset.color;

            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1f);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.controller;

            GetComponent<SpaceShip>().Use(asset);

            var collider = GetComponentInChildren<CircleCollider2D>();

            collider.radius = asset.Radius;

            collider.bounds.center.Set(0, 0, 0);
        }
    }

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
}