using SpaceShooter;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [SerializeField] private List<EpisodeScore> m_CompletionDate;

        [SerializeField] private List<Episode> m_Episodes;

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

            Saver<List<EpisodeScore>>.TryLoad(filename, ref m_CompletionDate);


            for (int i = 0; i < m_CompletionDate.Count; i++)
            {
                m_totalScore += m_CompletionDate[i].Score;
                m_Episodes[i].Id = m_CompletionDate[i].Id;
            }
        }

        private void OnValidate()
        {

            for (int i = 0; i < m_Episodes.Count; i++)
            {
                if (m_Episodes[i] == null)
                    continue;

                if (m_CompletionDate.Count <= i)
                {
                    var newEpisodeScore = new EpisodeScore();
                    m_CompletionDate.Add(newEpisodeScore);
                }

                if (!m_Episodes[i].Id.Equals(m_CompletionDate[i].Id))
                {
                    m_CompletionDate[i].Id = m_Episodes[i].Id;
                }
            }
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                foreach (var episodeScore in Instance.m_CompletionDate)
                {
                    if (episodeScore.Id.Equals(LevelSequenceController.Instance.CurrentEpisode.Id))
                    { //Сохранения новых очков прохождения.
                        if (levelScore >= episodeScore.Score)
                        {
                            episodeScore.Score = levelScore;
                        }
                    }
                }
                Saver<List<EpisodeScore>>.Save(filename, Instance.m_CompletionDate);

                Instance.m_totalScore = 0;

                foreach (var episodeScore in Instance.m_CompletionDate)
                {
                    Instance.m_totalScore += episodeScore.Score;
                }
            }
        }
        
        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in m_CompletionDate)
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