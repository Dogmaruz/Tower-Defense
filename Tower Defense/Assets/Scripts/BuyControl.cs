using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private AnimationBase m_AnimationSpriteScale;

        private RectTransform m_RectTransform;

        #region Unity Events
        private void Awake()
        {
            BuildSite.OnclickEvent += MoveToBuildSite;

            gameObject.SetActive(false);

            m_RectTransform = GetComponent<RectTransform>();
        }

        public void OnDestroy()
        {
            BuildSite.OnclickEvent -= MoveToBuildSite;
        }

        #endregion

        /// <summary>
        /// Перемещае позицию BuyControl в позицию BuildSide и воспроизводит анимацию появления.
        /// </summary>
        /// <param name="buildSite"></param>
        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);

                m_RectTransform.anchoredPosition = position;

                gameObject.SetActive(true);
            } 
            else
            {
                gameObject.SetActive(false);
            }

            foreach (var towerBuyControl in GetComponentsInChildren<TowerBuyControl>())
            {
                towerBuyControl.SetBuildSite(buildSite);
            }

            m_AnimationSpriteScale.StartAnimation(true);
        }
    }
}