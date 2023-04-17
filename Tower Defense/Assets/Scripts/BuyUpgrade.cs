using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_asset;

        [SerializeField] private Image m_upgradeIcon;

        [SerializeField] private Text m_level, m_cost;

        [SerializeField] private Button m_buyButton;

        public void Initialise()
        {
            m_upgradeIcon.sprite = m_asset.sprite;

            var savedLevel = Upgrades.GetUpgradeLevel(m_asset);

            m_level.text = $"Lvl: {savedLevel + 1}";

            m_cost.text = m_asset.costByLevel[savedLevel].ToString();
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(m_asset);
        }
    }
}