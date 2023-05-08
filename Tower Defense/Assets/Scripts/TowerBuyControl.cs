using SpaceShooter;
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

        [SerializeField] private AnimationBase m_AnimationSpriteScale;
        public AnimationBase AnimationSpriteScale { get => m_AnimationSpriteScale; set => m_AnimationSpriteScale = value; }

        public void SetTowerAsset(TowerAsset towerAsset)
        {
            m_towerAsset = towerAsset;
        }

        //Задает место для строительства башни.
        public void SetBuildSite(Transform value)
        {
            buildSite = value;

            m_AnimationSpriteScale.StartAnimation(true);
        }

        private void Start()
        {
            TD_Player.GoldUpdateSubscrible(GoldStatusCheck);

            m_text.text = m_towerAsset.GoldCost.ToString();

            m_button.GetComponent<Image>().sprite = m_towerAsset.GUISprite;

            var level = Upgrades.GetUpgradeLevel(m_towerUpgrade);

            if (level >= m_towerAsset.BuildLevel)
            {//Проверяет на доступность уровня строительства.
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        //Делает активной кнопку покупки башни при достаточном колличестве монет.
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_towerAsset.GoldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;

                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }

        //Производит покупку башни с строит ее на месте постоики. Уменьшает колличество монет и удаляет BuilSite.
        public void Buy()
        {
            Sound.TowerBuilding.Play();

            TD_Player.Instance.TryBuild(m_towerAsset, buildSite);

            BuildSite.HideControls();
        }

        private void OnDestroy()
        {//Отписываемся от события.
            TD_Player.GoldUpdateUnSubscrible(GoldStatusCheck);
        }
    }
}