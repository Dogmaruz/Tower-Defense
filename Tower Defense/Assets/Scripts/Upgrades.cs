using SpaceShooter;
using System;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefense.MapCompletion;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;

            public int level = 0;
        }

        [SerializeField] private UpgradeSave[] m_save;

        public const string filename = "upgrades.dat";

        private new void Awake()
        {
            base.Awake();

            Saver<UpgradeSave[]>.TryLoad(filename, ref m_save);
        }

        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgade in Instance.m_save)
            {
                if (upgade.asset == asset)
                {
                    upgade.level++;

                    Saver<UpgradeSave[]>.Save(filename, Instance.m_save);
                }
            }
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgade in Instance.m_save)
            {
                if (upgade.asset == asset)
                {
                    return upgade.level;
                }
            }

            return 0;
        }
    }
}