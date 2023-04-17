using SpaceShooter;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerDefense.MapCompletion;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [SerializeField] private List<EpisodeScore> m_completionDate;
        [SerializeField] private List<Episode> m_episodes;

        private int m_totalScore;
        public int TotalScore => m_totalScore;

        public const string filename = "completion.dat";

        [Serializable]
        public class EpisodeScore
        {
            // public Episode episode;

            public int Score;

            public string Id;
        }

        private new void Awake()
        {
            base.Awake();

            Saver<List<EpisodeScore>>.TryLoad(filename, ref m_completionDate);


            for (int i = 0; i < m_completionDate.Count; i++)
            {
                m_totalScore += m_completionDate[i].Score;
                m_episodes[i].Id = m_completionDate[i].Id;
            }
            
        }

        private void OnValidate()
        {
       
            for (int i = 0; i < m_episodes.Count; i++)
            {
                if (m_episodes[i] == null)
                    continue;

                if (m_completionDate.Count <= i)
                {
                    var newEpisodeScore = new EpisodeScore();
                    m_completionDate.Add(newEpisodeScore);
                }

                if (!m_episodes[i].Id.Equals(m_completionDate[i].Id))
                {
                    m_completionDate[i].Id = m_episodes[i].Id;
                }
            }

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
                if (episodeScore.Id.Equals(currentEpisode.Id))
                {
                    if (levelScore >= episodeScore.Score)
                    {
                        episodeScore.Score = levelScore;
                    }
                }
            }
            Saver<List<EpisodeScore>>.Save(filename, m_completionDate);

            m_totalScore = 0;

            foreach (var episodeScore in m_completionDate)
            {
                m_totalScore += episodeScore.Score;
            }
        }

        //public bool TryIndex(int id, out Episode episode, out int score)
        //{
        //    if (id >= 0 && id < m_completionDate.Count)
        //    {
        //        episode = m_episodes[id];

        //        score = m_completionDate[id].Score;

        //        return true;
        //    }

        //    episode = null;

        //    score = 0;

        //    return false;
        //}

        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in m_completionDate)
            {
                if (data.Id == m_episode.Id)
                {
                    return data.Score;
                }
            }

            return 0;
        }
    }
}