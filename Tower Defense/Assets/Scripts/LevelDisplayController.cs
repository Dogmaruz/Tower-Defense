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
            for (int i = 0; i < m_Levels.Length; i++)
            {
                m_Levels[i].Initialize(m_openSprite, m_closedSprite, i, m_Levels);
            }

            for (int i = 0; i < m_branchLevels.Length; i++)
            {
                if (m_branchLevels[i].RootIsActive)
                {
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