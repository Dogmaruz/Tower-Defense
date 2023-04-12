using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private Sprite m_closedSprite;

        [SerializeField] private Sprite m_openSprite;

        [SerializeField] private Map_Level[] m_Levels;

        private void Start()
        {
            var drawLevel = 0;

            var score = 1;

            while (score != 0 && drawLevel < m_Levels.Length && MapCompletion.Instance.TryIndex(drawLevel, out Episode episode, out score))
            {
                Debug.Log($"{episode} : {score}");

                m_Levels[drawLevel].SetLevelData(episode, score);

                m_Levels[drawLevel].SetImage(m_openSprite);

                m_Levels[drawLevel].IsInteractive = true;

                drawLevel++;
            }

            for (int i = drawLevel; i < m_Levels.Length; i++)
            {
                //m_Levels[i].gameObject.SetActive(false);

                m_Levels[i].SetImage(m_closedSprite);

                m_Levels[i].IsInteractive = false;
            }
        }
    }
}