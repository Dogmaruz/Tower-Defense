﻿using SpaceShooter;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [SerializeField] private EpisodeScore[] m_completionDate;

        const string filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;

            public int Score;
        }

        private new void Awake()
        {
            base.Awake();

            Saver<EpisodeScore[]>.TryLoad(filename, ref m_completionDate);
        }

        public void SaveEpisodeResult(int levelScore)
        {
            SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
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

                        Saver<EpisodeScore[]>.Save(filename, m_completionDate);
                    }
                }
            }
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