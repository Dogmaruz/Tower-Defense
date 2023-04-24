using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_Asset;
        public UpgradeAsset Asset { get => m_Asset; }

        [SerializeField] private Image m_UpgradeIcon;

        [SerializeField] private Text m_Level, m_CostText, m_name;

        [SerializeField] private Button m_BuyButton;

        [SerializeField] private int m_AccessLevel;

        private int m_CostNumber = 0;


        /// <summary>
        /// Инициализация слота покупки в зависимости от его уровня.
        /// Активируется при достаточном уровне строительства.
        /// </summary>
        public void Initialize()
        {
            m_UpgradeIcon.sprite = m_Asset.sprite;

            var savedLevel = Upgrades.GetUpgradeLevel(m_Asset);

            if (savedLevel >= m_Asset.costByLevel.Length)
            {
                m_Level.text = $"Lvl: {savedLevel + 1} (Max)";

                m_name.text = m_Asset.Name;

                m_BuyButton.interactable = false;

                m_BuyButton.transform.Find("Star_Image").gameObject.SetActive(false);

                m_BuyButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "No Active";

                m_CostText.text = "";

                m_CostNumber = int.MaxValue;

            }
            else
            {
                m_Level.text = $"Lvl: {savedLevel + 1}";

                m_CostNumber = m_Asset.costByLevel[savedLevel];

                m_CostText.text = m_CostNumber.ToString();

                m_name.text = m_Asset.Name;
            }
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(m_Asset);

            Initialize();
        }

        public void CheckCost(int money)
        {
            m_BuyButton.interactable = money >= m_CostNumber;
        }

        public bool CheckLevel(UpgradeAsset m_TowerUpgrades)
        {
            return m_BuyButton.interactable = Upgrades.GetUpgradeLevel(m_TowerUpgrades) + 1 >= m_AccessLevel;
        }
    }
}