using UnityEngine;

namespace Assets.Scripts.ScoreSystem
{
    public class ScoreRepositoryToPlayerPrefs : IScoreRepository
    {
        public void SaveBestScore(int score)
        {
            PlayerPrefs.SetInt("best_score", score);
        }

        public int LoadBestScore()
        {
            var score = PlayerPrefs.GetInt("best_score");
            return score > 0 ? score : 0;
        }
    }
}