using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_rectTransform;
        private void Awake()
        {
            BuildSite.OnclickEvent += MoveToBuildSite;
            gameObject.SetActive(false);

            m_rectTransform = GetComponent<RectTransform>();
        }

        private void OnDestroy()
        {
            BuildSite.OnclickEvent -= MoveToBuildSite;
        }

        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                m_rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
            } else
            {
                gameObject.SetActive(false);
            }

            foreach (var towerBuyControl in GetComponentsInChildren<TowerBuyControl>())
            {
                towerBuyControl.SetBuildSite(buildSite);
            }
        }
    }
}