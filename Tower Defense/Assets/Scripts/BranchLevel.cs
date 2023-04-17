using SpaceShooter;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(Map_Level))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private Map_Level m_rootLevel;

        [SerializeField] private Text m_pointText;

        [SerializeField] private int m_needPoints = 3;

        private Map_Level m_mapLevel;

        public bool RootIsActive { get { return m_rootLevel.IsCompleted; } }

        private void Start()
        {
            m_pointText.text = m_needPoints.ToString();

            m_mapLevel = GetComponent<Map_Level>();
        }

        internal void TryActivate(Sprite openSprite)
        {
            if (m_needPoints <= MapCompletion.Instance.TotalScore)
            {
                m_pointText.transform.parent.gameObject.SetActive(false);

                GetComponent<Map_Level>().SetImage(openSprite);

                GetComponent<Map_Level>().IsInteractive = true;

                var score = MapCompletion.Instance.GetEpisodeScore(m_mapLevel.Episode);

                if (score > 0) m_mapLevel.IsCompleted = true;

                for (int i = 0; i < score; i++)
                {
                    m_mapLevel.StarsImages[i].color = new Color(255, 255, 255, 255);
                }
            }
        }
    }
}