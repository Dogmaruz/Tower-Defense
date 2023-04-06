using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{

    public class StartEpisode : MonoBehaviour
    {
        [SerializeField] private LevelSequenceController m_levelSequenceController;
        [SerializeField] private Episode m_episode;

        private void Awake()
        {
            m_levelSequenceController.StartEpisode(m_episode);
        }
    }
}