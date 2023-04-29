namespace TowerDefense
{
    public enum Sound
    {
        ArrowProjectile = 0,
        MagicProjectile = 1,
        StoneProjecctile = 2,
        ArrowHit = 3,
        NextWave = 4,
        Victory = 5,
        Loss = 6,
        EnemyDie = 7,
        EndPath = 8,
        TowerBuilding = 9,
        BuyTower = 10,
        TowerUpgrade = 11,
        NextWaveButton = 12,
        WaveIsDath = 13,
        MagicHit = 14,
        StoneHit = 15,
        Click = 16
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound) 
        {
            SoundPlayer.Instance.Play(sound);
        }
    }

}