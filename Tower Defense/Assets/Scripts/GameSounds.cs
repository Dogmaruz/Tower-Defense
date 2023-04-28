using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "GameSounds", menuName = "Sounds/GameSounds")]
    public class GameSounds : ScriptableObject
    {
        public AudioClip[] m_Sounds;

        public AudioClip this[Sound sound] => m_Sounds[(int)sound];
    }
}