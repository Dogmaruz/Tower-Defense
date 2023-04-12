using SpaceShooter;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Map_Level : MonoBehaviour
    {
        private Episode m_episode;

        [SerializeField] private RectTransform m_ResultPanel;

        [SerializeField] private SpriteRenderer m_Image;
        [SerializeField] private Image[] m_starsImages;

        public bool IsInteractive;

        public void LoadLevel()
        {
            if (!IsInteractive) return;

            LevelSequenceController.Instance.StartEpisode(m_episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            m_episode = episode;

            for (int i = 0; i < score; i++)
            {
                m_starsImages[i].color = new Color(255, 255, 255, 255);
            }
        }

        public void SetImage(Sprite sprite)
        {
            m_Image.sprite = sprite;
        }
    }
}