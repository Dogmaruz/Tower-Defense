using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Life,
            Score,
            Manna
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
                case UpdateSource.Score:
                    TD_Player.ScoreUpdateSubscrible(UpdateText);
                    break;
                case UpdateSource.Manna:
                    TD_Player.MannaUpdateSubscrible(UpdateText);
                    break;
            }
        }

        private void UpdateText(int value)
        {
            m_text.text = value.ToString();
        }

        private void OnDestroy()
        {
            TD_Player.GoldUpdateUnSubscrible(UpdateText);

            TD_Player.LifeUpdateUnSubscrible(UpdateText);

            TD_Player.ScoreUpdateUnSubscrible(UpdateText);

            TD_Player.MannaUpdateUnSubscrible(UpdateText);
        }
    }
}