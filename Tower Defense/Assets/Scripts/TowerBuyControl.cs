using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_towerAsset;

        [SerializeField] private Text m_text;

        [SerializeField] private Button m_button;

        [SerializeField] private Transform buildSite;

        [SerializeField] private UpgradeAsset m_towerUpgrade;

        public void SetBuildSite(Transform value)
        {
            buildSite = value;
        }

        private void Start()
        {
            TD_Player.GoldUpdateSubscrible(GoldStatusCheck);

            m_text.text = m_towerAsset.GoldCost.ToString();

            m_button.GetComponent<Image>().sprite = m_towerAsset.GUISprite;

            var level = Upgrades.GetUpgradeLevel(m_towerUpgrade);

            if (level >= m_towerAsset.Level)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }


        }
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_towerAsset.GoldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;

                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TD_Player.Instance.TryBuild(m_towerAsset, buildSite);

            BuildSite.HideControls();
        }

        private void OnDestroy()
        {
            TD_Player.GoldUpdateUnSubscrible(GoldStatusCheck);
        }
    }
}