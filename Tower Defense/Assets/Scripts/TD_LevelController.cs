using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class TD_LevelController : LevelController
    {
        private new void Start()
        {
            base.Start();

            TD_Player.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();

                LevelResultController.Instance.ShowResults(false);
            };

            m_EventLevelCompleted.AddListener(StopLevelActivity);
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