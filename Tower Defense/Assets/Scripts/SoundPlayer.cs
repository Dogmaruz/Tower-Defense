using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private GameSounds m_Sounds;

        [SerializeField] private AudioClip m_Background;

        private AudioSource m_AudioSource;
        public AudioSource AudioSource { get => m_AudioSource; set => m_AudioSource = value; }

        private new void Awake()
        {
            base.Awake();

            m_AudioSource = GetComponent<AudioSource>();

            Instance.m_AudioSource.clip = m_Background;

            Instance.m_AudioSource.volume = 0.2f;

            Instance.m_AudioSource.Play();

        }
        
        public void Play(Sound sound)
        {
            m_AudioSource.PlayOneShot(m_Sounds[sound]);
        }
    }
}