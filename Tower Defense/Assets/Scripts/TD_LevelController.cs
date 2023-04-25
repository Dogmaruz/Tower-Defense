using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class TD_LevelController : LevelController
    {

        private int LevelScore = 3;

        private new void Start()
        {
            base.Start();

            TD_Player.Instance.OnPlayerDead += () =>
            {
                
                StopLevelActivity();

                LevelResultController.Instance.ShowResults(false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(() =>
            {
                
                StopLevelActivity();

                if (m_ReferenceTime < Time.time) LevelScore -= 1;

                MapCompletion.SaveEpisodeResult(LevelScore);
            });

            TD_Player.OnLifeUpdate += LifeScoreChange;

            void LifeScoreChange(int _)
            {
                LevelScore -= 1;

                TD_Player.OnLifeUpdate -= LifeScoreChange;
            }
        }

        //Останавливает время на сцене.
        private void StopLevelActivity()
        {
            foreach (var destructible in Destructible.AllDestructible)
            {
                destructible.GetComponent<TD_PatrolController>().SetNavigationLinear(0);
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();

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