using SpaceShooter;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyControlPrefabe;

        [SerializeField] private AnimationBase m_AnimationSpriteScale;

        [SerializeField] private UpgradeAsset m_UpgradeTowerBuild;

        [SerializeField] private Transform m_TargetPosition;

        private List<TowerBuyControl> m_ActivControl;

        private RectTransform m_RectTransform;

        #region Unity Events
        private void Awake()
        {
            BuildSite.OnclickEvent += MoveToBuildSite;

            gameObject.SetActive(false);

            m_RectTransform = GetComponent<RectTransform>();

            m_ActivControl = new List<TowerBuyControl>();
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
        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);

                m_RectTransform.anchoredPosition = position;

                gameObject.SetActive(true);

                foreach (var control in m_ActivControl)
                {
                    if (control != null)
                    {
                        Destroy(control.gameObject);
                    }
                }

                m_ActivControl.Clear();

                for (int i = 0; i < buildSite.TowerAssets.Count; i++)
                {
                    if (buildSite.TowerAssets[i].IsAvailableToBuild(m_UpgradeTowerBuild))
                    {
                        var newControl = Instantiate(m_TowerBuyControlPrefabe, m_TargetPosition.position, Quaternion.identity, this.transform);

                        newControl.SetTowerAsset(buildSite.TowerAssets[i]);

                        newControl.GetComponent<AnimationSpriteScale>().SetAnimationTime(i * 0.2f);

                        m_ActivControl.Add(newControl);
                    }
                }

                int angle;

                switch (m_ActivControl.Count)
                {
                    case 1: angle = 90;
                        break;
                    case 2: angle = 180;
                        break;
                    case 3: angle = 90;
                        break;
                    case 4: angle = 90;
                        break;
                    default: angle = 360 / m_ActivControl.Count;
                        break;
                }

                for (int i = 0; i < m_ActivControl.Count; i++)
                {
                    var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.right * 100);

                    m_ActivControl[i].transform.position += offset;
                }

                foreach (var towerBuyControl in GetComponentsInChildren<TowerBuyControl>())
                {
                    towerBuyControl.SetBuildSite(buildSite.transform.root);
                }

                m_AnimationSpriteScale.StartAnimation(true);
            }
            else
            {
                foreach (var control in m_ActivControl)
                {
                    if (control != null)
                    {
                        Destroy(control.gameObject);
                    }
                }

                m_ActivControl.Clear();

                gameObject.SetActive(false);
            }
        }
    }
}