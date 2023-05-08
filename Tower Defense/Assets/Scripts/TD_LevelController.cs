using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class TD_LevelController : LevelController
    {
        private int LevelScore = 3;
        public bool IsStopLevelActivity { get; private set;}

        public static new TD_LevelController Instance
        {
            get
            {
                return LevelController.Instance as TD_LevelController;
            }
        }

        private new void Start()
        {
            base.Start();

            TD_Player.Instance.OnPlayerDead += () =>
            {//Подписываемся на событие смерти игрока.
                StopLevelActivity();

                LevelResultController.Instance.ShowResults(false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(() =>
            {//Подписываемся на событие прохождения уровня.
                StopLevelActivity();

                //Уменьшается на одну звезду если время уровня вышло.
                if (m_ReferenceTime < Time.time) LevelScore -= 1;

                MapCompletion.SaveEpisodeResult(LevelScore);
            });

            TD_Player.OnLifeUpdate += LifeScoreChange;

            void LifeScoreChange(int _)
            {//Метод вызывается один раз и производиться отписка от события, при прорыве врага. Уменьшается на одну звезду.
                LevelScore -= 1;

                TD_Player.OnLifeUpdate -= LifeScoreChange;
            }
        }

        //Останавливает время на сцене.
        public void StopLevelActivity()
        {
            foreach (var destructible in Destructible.AllDestructible)
            {
                destructible.GetComponent<TD_PatrolController>().SetNavigationLinear(0);
            }

            DisableAll<EnemyWawe>();

            DisableAll<Projectile>();

            DisableAll<Tower>();

            DisableAll<NextWaveGUI>();

            IsStopLevelActivity = true;

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
        }
    }
}