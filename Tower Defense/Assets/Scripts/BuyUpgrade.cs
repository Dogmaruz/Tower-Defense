using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_asset;

        [SerializeField] private Image m_upgradeIcon;

        [SerializeField] private Text m_level, m_costText, m_name;

        [SerializeField] private Button m_buyButton;

        private int m_costNumber = 0;

        public void Initialise()
        {
            m_upgradeIcon.sprite = m_asset.sprite;

            var savedLevel = Upgrades.GetUpgradeLevel(m_asset);



            if (savedLevel >= m_asset.costByLevel.Length)
            {
                m_level.text = $"Lvl: {savedLevel + 1} (Max)";

                m_name.text = m_asset.Name;

                m_buyButton.interactable = false;

                m_buyButton.transform.Find("Star_Image").gameObject.SetActive(false);

                m_buyButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "No Active";

                m_costText.text = "";

                m_costNumber = int.MaxValue;

            }
            else
            {
                m_level.text = $"Lvl: {savedLevel + 1}";

                m_costNumber = m_asset.costByLevel[savedLevel];

                m_costText.text = m_costNumber.ToString();

                m_name.text = m_asset.Name;
            }

        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(m_asset);

            Initialise();
        }

        public void CheckCost(int money)
        {
            m_buyButton.interactable = money >= m_costNumber;
        }
    }
}