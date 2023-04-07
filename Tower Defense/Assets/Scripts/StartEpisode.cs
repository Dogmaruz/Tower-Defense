using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{

    public class StartEpisode : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;

        private void Start()
        {
            LevelSequenceController.Instance.StartEpisode(m_episode);
        }
    }
}