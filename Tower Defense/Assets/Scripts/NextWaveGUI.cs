using SpaceShooter;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text m_bonusAmount;

        private EnemyWaveManager m_Manager;

        private float m_timeToNextWave;

        private void Start()
        {
            m_Manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWawe.OnWavePrepare += (float time) =>
            {
                m_timeToNextWave = time;
            };
        }

        public void CallWave()
        {
            m_Manager.ForceNextWave();
        }

        private void Update()
        {
            m_timeToNextWave -= Time.deltaTime;

            m_bonusAmount.text = ((int)m_timeToNextWave < 0 ? 0 : (int)m_timeToNextWave).ToString();
        }
    }
}
