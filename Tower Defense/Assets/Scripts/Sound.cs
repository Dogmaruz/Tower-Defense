using UnityEngine;

namespace TowerDefense
{
    public enum Sound
    {
        ArrowProjectile,
        MagicProjectile,
        StoneProjecctile,
        ArrowHit,
        NextWave
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound) 
        {
            SoundPlayer.Instance.Play(sound);
        }
    }

}