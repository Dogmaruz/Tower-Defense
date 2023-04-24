using SpaceShooter;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "TowerAsset", menuName = "Tower/TowerAsset")]
    [System.Serializable]
    public class TowerAsset : ScriptableObject
    {
        public int GoldCost = 15;
        public Sprite GUISprite;
        public Sprite ElementSprite;
        public TurretProperties TurretProperties;
        public GameObject EffectPrefab;
        public UpgradeAsset m_UpgradeAsset;
        public int UpgradeLevel;
        public int BuildLevel;
        public List<TowerAsset> UpgadesTo;

        public bool IsAvailableToBuild(UpgradeAsset upgradeTowerBuild) =>
                                Upgrades.GetUpgradeLevel(upgradeTowerBuild) >= BuildLevel;

        public bool IsAvailableToUpgrade() =>
                                Upgrades.GetUpgradeLevel(m_UpgradeAsset) >= UpgradeLevel;
    }
}