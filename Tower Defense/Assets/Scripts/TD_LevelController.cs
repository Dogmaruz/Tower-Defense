using SpaceShooter;

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

                ResultPanelController.Instance.ShowResults(false);
            };
        }

        private void StopLevelActivity()
        {

        }
    }
}