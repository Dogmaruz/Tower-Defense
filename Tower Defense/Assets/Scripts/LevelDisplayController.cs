using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private Sprite m_closedSprite;

        [SerializeField] private Sprite m_openSprite;

        [SerializeField] private Map_Level[] m_Levels;

        [SerializeField] private BranchLevel[] m_branchLevels;

        private void Start()
        {
            //var drawLevel = 0;

            //var score = 1;

            for (int i = 0; i < m_Levels.Length; i++)
            {
                m_Levels[i].Initialize(m_openSprite, m_closedSprite, i, m_Levels);

                //drawLevel++;
            }

            //for (int i = drawLevel; i < m_Levels.Length; i++)
            //{
            //    //m_Levels[i].gameObject.SetActive(false);

            //    m_Levels[i].SetImage(m_closedSprite);

            //    m_Levels[i].IsInteractive = false;
            //}

            for (int i = 0; i < m_branchLevels.Length; i++)
            {
                if (m_branchLevels[i].RootIsActive)
                {
                    //m_branchLevels[i].GetComponent<Map_Level>().SetImage(m_openSprite);

                    //m_branchLevels[i].GetComponent<Map_Level>().IsInteractive = true;

                    m_branchLevels[i].TryActivate(m_openSprite);
                }
                else
                {
                    m_branchLevels[i].GetComponent<Map_Level>().SetImage(m_closedSprite);

                    m_branchLevels[i].GetComponent<Map_Level>().IsInteractive = false;
                }
            }
        }
    }
}