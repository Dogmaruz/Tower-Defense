using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "UpgradeAsset", menuName = "Upgrade/UpgradeAsset")]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite sprite;

        public int[] costByLevel = { 3 };
    }
}