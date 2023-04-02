using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Life
        }

        public UpdateSource Source = UpdateSource.Gold;

        private Text m_text;
        private void Awake()
        {
            m_text = GetComponent<Text>();

            switch (Source)
            {
                case UpdateSource.Gold:
                    TD_Player.OnGoldUpdate += UpdateText;
                    break;
                case UpdateSource.Life:
                    TD_Player.OnLifeUpdate += UpdateText;
                    break;
            }

        }

        private void UpdateText(int money)
        {
            m_text.text = money.ToString();
        }
    }
}