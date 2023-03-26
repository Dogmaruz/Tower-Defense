using UnityEngine;

namespace TowerDefense
{
    
    [CreateAssetMenu(fileName = "EnemyAsset", menuName = "Enemy/EnemyAsset")]
    public sealed class EnemyAsset : ScriptableObject
    {
       public Color color = Color.white;
    }
}