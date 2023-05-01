using System;
using TowerDefense;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LevelResultController : SingletonBase<LevelResultController>
    {
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;
        [SerializeField] private AnimationBase m_AnimationSpriteScale;
        [SerializeField] private Image m_menuImage;
        [SerializeField] private Sprite m_loseSprite;
        [SerializeField] private Sprite m_winSprite;

        public event Action OnShowPanel;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;

            SoundPlayer.Instance.AudioSource.Stop();

            Sound sound = success ? Sound.Victory : Sound.Loss;

            sound.Play();

            m_Result.text = success ? "Уровень пройден" : "Ты проиграл";

            m_menuImage.sprite = success ? m_winSprite : m_loseSprite;

            m_ButtonNextText.text = success ? "Продолжить" : "Начать заново";

            m_AnimationSpriteScale.StartAnimation(true);

            OnShowPanel?.Invoke();
        }

        public void OnButtonNextAction()
        {
            SoundPlayer.Instance.AudioSource.Stop();

            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}