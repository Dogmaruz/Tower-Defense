using SpaceShooter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{

    public class WindowNavigationController : MonoBehaviour
    {
        [SerializeField] private AnimationBase m_AnimationSpriteScale;

        private void Awake()
        {
            if (m_AnimationSpriteScale != null)
            {
                m_AnimationSpriteScale.StartAnimation(true);
            }
        }
        public void OnButtonContinue()
        {
            SoundPlayer.Instance.AudioSource.Stop();

            Time.timeScale = 1;

            LevelSequenceController.Instance.AdvanceLevel();
        }


        public void OnButtonMainMenu()
        {
            SoundPlayer.Instance.AudioSource.Stop();

            Time.timeScale = 1;

            SceneManager.LoadScene(0);
        }

    }
}