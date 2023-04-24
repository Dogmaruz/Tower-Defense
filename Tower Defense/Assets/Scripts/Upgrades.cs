using SpaceShooter;
using System;
using System.Collections.Generic;
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

        [SerializeField] private List<UpgradeAsset> m_UpgradeAssets;

        [SerializeField] private UpgradeShop m_UpgradeShop;

        public const string filename = "upgrades.dat";

        private void OnValidate()
        {
            if (m_UpgradeAssets.Count < m_UpgradeShop.Sales.Count || m_UpgradeAssets.Count > m_UpgradeShop.Sales.Count)
            {
                m_UpgradeAssets.Clear();

                for (int i = 0; i < m_UpgradeShop.Sales.Count; i++)
                {
                    m_UpgradeAssets.Add(m_UpgradeShop.Sales[i].Asset);
                }
            }
        }

        private new void Awake()
        {
            base.Awake();

            if (m_save.Length == 0)
            {
                m_save = new UpgradeSave[m_UpgradeAssets.Count];

                for (int i = 0; i < m_save.Length; i++)
                {
                    m_save[i] = new UpgradeSave();

                    m_save[i].asset = m_UpgradeAssets[i];

                    m_save[i].Id = m_UpgradeAssets[i].Id;
                }
            }

            Saver<UpgradeSave[]>.TryLoad(filename, ref m_save);

            foreach (var save in m_save)
            {
                save.UpdateId();
            }

            foreach (var asset in m_UpgradeAssets)
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