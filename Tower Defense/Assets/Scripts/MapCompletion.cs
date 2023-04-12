using SpaceShooter;
using System;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [SerializeField] private EpisodeScore[] m_completionDate;

        public const string filename = "completion.dat";

        [Serializable]
        public class EpisodeScore
        {
            public Episode episode;

            public int Score;
        }

        private new void Awake()
        {
            base.Awake();

            Saver<EpisodeScore[]>.TryLoad(filename, ref m_completionDate);
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var episodeScore in m_completionDate)
            {
                if (episodeScore.episode == currentEpisode)
                {
                    if (levelScore >= episodeScore.Score)
                    {
                        episodeScore.Score = levelScore;
                    }
                }
            }
            Saver<EpisodeScore[]>.Save(filename, m_completionDate);
        }

        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < m_completionDate.Length)
            {
                episode = m_completionDate[id].episode;

                score = m_completionDate[id].Score;

                return true;
            }

            episode = null;

            score = 0;

            return false;
        }
    }
}