using SpaceShooter;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Map_Level : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        public Episode Episode { get => m_episode; }

        [SerializeField] private RectTransform m_ResultPanel;

        [SerializeField] private SpriteRenderer m_Image;

        [SerializeField] private Image[] m_starsImages;
        public Image[] StarsImages { get => m_starsImages; }

        public bool IsCompleted;

        public bool IsInteractive;


        public void LoadLevel()
        {
            if (!IsInteractive) return;

            LevelSequenceController.Instance.StartEpisode(m_episode);
        }

        public void SetImage(Sprite sprite)
        {
            m_Image.sprite = sprite;
        }

        //Инициализация отрисовкки уровня на карте.
        public void Initialize(Sprite openSprite, Sprite closedSprite, int index, Map_Level[] m_Levels)
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_episode);

            if (score > 0) IsCompleted = true;

            for (int i = 0; i < score; i++)
            {
                m_starsImages[i].color = new Color(255, 255, 255, 255);
            }

            if (index == 0)
            {
                SetImage(openSprite);

                IsInteractive = true;
            }
            else
            {
                if (m_Levels[index - 1].IsCompleted)
                {
                    SetImage(openSprite);

                    IsInteractive = true;
                }
                else
                {
                    SetImage(closedSprite);

                    IsInteractive = false;
                }
            }
        }
    }
}