using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button m_continueButton;
        [SerializeField] private GameObject m_confirtionPanel;
        [SerializeField] private GameObject m_mainMenu;
        private void Start()
        {
            m_continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
        }
        public void NewGame()
        {
            if (FileHandler.HasFile(MapCompletion.filename))
            {
                m_mainMenu.SetActive(false);

                m_confirtionPanel.SetActive(true);
            }
            else
            {
                ConfirmationNewGame();
            }
        }

        public void ConfirmationNewGame()
        {
            m_confirtionPanel.SetActive(false);

            FileHandler.Reset(MapCompletion.filename);

            Continue();
        }

        public void CancelationNewGame()
        {
            m_confirtionPanel.SetActive(false);

            m_mainMenu.SetActive(true);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}