using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private List<TowerAsset> m_TowerAssets;
        public List<TowerAsset> TowerAssets { get => m_TowerAssets; }

        public static event Action<BuildSite> OnclickEvent;

        public void SetBuiddableTowers(List<TowerAsset> towerAssets)
        {
            if (towerAssets == null || towerAssets.Count == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                foreach (var asset in towerAssets)
                {
                    if (asset.IsAvailableToUpgrade())
                    {
                        m_TowerAssets.Add(asset);
                    }
                }

                if (m_TowerAssets.Count == 0)
                {
                    Destroy(transform.parent.gameObject);
                }
                //m_TowerAssets = towerAssets;
            }
        }

        public static void HideControls()
        {
            OnclickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnclickEvent(this);
        }
    }
}