using UnityEngine;
using System;
# if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "GameSounds", menuName = "Sounds/GameSounds")]
    public class GameSounds : ScriptableObject
    {
        public AudioClip[] m_Sounds;

        public AudioClip this[Sound sound] => m_Sounds[(int)sound];

#if UNITY_EDITOR
        [CustomEditor(typeof(GameSounds))]
        public class SoundsInspector : Editor
        {
            private static readonly int soundCount = Enum.GetValues(typeof(Sound)).Length;
            private new GameSounds target => base.target as GameSounds;

            //public override void OnInspectorGUI()
            //{
            //    if (target.m_sounds.length < soundcount)
            //    {
            //        array.resize(ref target.m_sounds, soundcount);
            //    }

            //    for (int i = 0; i < target.m_sounds.length; i++)
            //    {
            //        target.m_sounds[i] = editorguilayout.objectfield($"{(sound)i} : ", target.m_sounds[i], typeof(audioclip), false) as audioclip;
            //    }

            //    EditorUtility.SetDirty(target);
            //}
        }
#endif
    }
}