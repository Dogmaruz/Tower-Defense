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
        private void Start()
        {
            m_text = GetComponent<Text>();

            switch (Source)
            {
                case UpdateSource.Gold:
                    TD_Player.GoldUpdateSubscrible(UpdateText);
                    break;
                case UpdateSource.Life:
                    TD_Player.LifeUpdateSubscrible(UpdateText);
                    break;
            }

        }

        private void UpdateText(int money)
        {
            m_text.text = money.ToString();
        }

        private void OnDestroy()
        {
            TD_Player.GoldUpdateUnSubscrible(UpdateText);
            TD_Player.LifeUpdateUnSubscrible(UpdateText);
        }
    }
}