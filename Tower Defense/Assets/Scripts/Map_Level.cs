using SpaceShooter;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Map_Level : MonoBehaviour
    {
        private Episode m_episode;

        [SerializeField] private Text m_text;

        [SerializeField] private SpriteRenderer m_image;

        public bool IsInteractive;

        public void LoadLevel()
        {
            if (!IsInteractive) return;

            LevelSequenceController.Instance.StartEpisode(m_episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            m_episode = episode;

            m_text.text = $"{score}/3";
        }

        public void SetImage(Sprite sprite)
        {
            m_image.sprite = sprite;
        }
    }
}