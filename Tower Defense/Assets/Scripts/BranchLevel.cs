using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(Map_Level))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private Map_Level m_RootLevel;

        [SerializeField] private Text m_PointText;

        [SerializeField] private int m_NeedPoints = 3; //Необходимое колличество звезд.

        private Map_Level m_MapLevel;

        public bool RootIsActive 
        { 
            get 
            { 
                return m_RootLevel.IsCompleted; 
            } 
        }

        private void Awake()
        {
            m_PointText.text = m_NeedPoints.ToString();

            m_MapLevel = GetComponent<Map_Level>();
        }

        /// <summary>
        /// Попытка активации ответвленного уровня.
        /// Активация требует наличия колличества звезд(очков) и выполнения основного уровня в ветке.
        /// </summary>
        /// <param name="openSprite">Картинка открытого спрайта</param>
        public void TryActivate(Sprite openSprite)
        {
            if (m_NeedPoints <= MapCompletion.Instance.TotalScore)
            {
                m_PointText.transform.parent.gameObject.SetActive(false);

                m_MapLevel.SetImage(openSprite);

                m_MapLevel.IsInteractive = true;

                var score = MapCompletion.Instance.GetEpisodeScore(m_MapLevel.Episode);

                if (score > 0) m_MapLevel.IsCompleted = true;

                for (int i = 0; i < score; i++)
                {
                    m_MapLevel.StarsImages[i].color = new Color(255, 255, 255, 255);
                }
            }
        }
    }
}