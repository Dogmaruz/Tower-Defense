using UnityEngine;

namespace TowerDefense
{

    [CreateAssetMenu(fileName = "EnemyAsset", menuName = "Enemy/EnemyAsset")]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]

        public Color color = Color.white;

        public Vector2 spriteScale = new Vector2 (3, 3);

        public RuntimeAnimatorController controller;

        [Header("Игровые параметры")]

        public float moveSpeed = 1f;

        public int HitPoints = 1;

        public int ScoreValue = 1;

        public float Radius = 0.22f;
    }
}