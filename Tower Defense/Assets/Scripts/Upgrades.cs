using SpaceShooter;
using System;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;

            public int level = 0;

            public string Id;

            public void UpdateId()
            {
                if (Id == "")
                {
                    Id = asset.Id;
                }
            }
        }

        [SerializeField] private UpgradeSave[] m_save;

        [SerializeField] private UpgradeAsset[] m_upgradeAssets;

        public const string filename = "upgrades.dat";

        private new void Awake()
        {
            base.Awake();

            Saver<UpgradeSave[]>.TryLoad(filename, ref m_save);

            foreach (var save in m_save)
            {
                save.UpdateId();
            }

            foreach (var asset in m_upgradeAssets)
            {
                foreach (var save in m_save)
                {
                    if (save.Id == asset.Id)
                    {
                        save.asset = asset;
                    }
                }

                asset.UpdateGUI();
            }
        }

        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgade in Instance.m_save)
            {
                if (upgade.Id == asset.Id)
                {
                    upgade.level++;

                    //upgade.Id = asset.Id;

                    Saver<UpgradeSave[]>.Save(filename, Instance.m_save);
                }
            }
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgade in Instance.m_save)
            {

                if (upgade.Id == asset.Id)
                {
                    return upgade.level;
                }
            }

            return 0;
        }

        public static int GetTotalCost()
        {
            int result = 0;

            foreach (var upgrade in Instance.m_save)
            {
                for (int i = 0; i < upgrade.level; i++)
                {
                    result += upgrade.asset.costByLevel[i];
                }
            }

            return result;
        }
    }
}