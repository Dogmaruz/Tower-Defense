using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text m_bonusAmount;

        [SerializeField] private Image m_BonusAmountProgressBar;

        [SerializeField] private Button m_ButtonNext;

        private float m_FillAmountStep;

        private EnemyWaveManager m_Manager;

        private float m_timeToNextWave;

        private void Start()
        {
            m_BonusAmountProgressBar.fillAmount = 1;

            m_Manager = FindObjectOfType<EnemyWaveManager>();

            EnemyWawe.OnWavePrepare += (float time, bool isWavesOver) =>
            {//При старте новой волны обновляем время до начала старта новой волны.
                if (isWavesOver)
                {//Если волны закончились блокируем кнопку вызова новой волны.
                    m_ButtonNext.interactable = false;
                }

                m_timeToNextWave = time;

                m_FillAmountStep = 1f / m_timeToNextWave;
            };
        }

        //Вызов волны. Вызывается в инспекторе по нажатию кнопки.
        public void CallWave()
        {
            m_Manager.ForceNextWave(true);
        }

        private void Update()
        {//Обновляем текст и полосу прогресса начала до следующей волны.
            m_timeToNextWave -= Time.deltaTime;

            m_bonusAmount.text = ((int)m_timeToNextWave < 0 ? 0 : (int)m_timeToNextWave).ToString();

            m_BonusAmountProgressBar.fillAmount = m_timeToNextWave * m_FillAmountStep;
        }
    }
}
