using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Map_Level : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_episode);
        }
    }
}