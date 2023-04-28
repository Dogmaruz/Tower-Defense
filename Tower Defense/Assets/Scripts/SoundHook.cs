using UnityEngine;

namespace TowerDefense
{

    public class SoundHook : MonoBehaviour
    {
        public Sound m_sound;

        public void Play()
        {
            m_sound.Play();
        }
    }
}