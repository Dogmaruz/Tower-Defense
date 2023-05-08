using UnityEngine;
using System;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "UpgradeAsset", menuName = "Upgrade/UpgradeAsset")]

    [Serializable]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public string Name;

        public Sprite sprite;

        public string Id;

        public int[] costByLevel = { 3 };

        //��������� ������������ ID.
        public void UpdateGUI()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }
}