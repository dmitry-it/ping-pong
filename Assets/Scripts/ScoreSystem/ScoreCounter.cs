using UnityEngine;

namespace Assets.Scripts.ScoreSystem
{
    public class ScoreCounter
    {
        public int CurrentScore => _score;
        public int MaxScore => _repository.LoadBestScore();
        private int _score = 0;
        private IScoreRepository _repository;
        public ScoreUpdateEvent OnScoreUpdateEvent = new ScoreUpdateEvent();

        public ScoreCounter(IScoreRepository repository)
        {
            this._repository = repository;
        }

        public void AddPointsToScore(int points)
        {
            _score += points;
            OnScoreUpdateEvent.Invoke(_score);
            
        }

        public void SaveResult()
        {
            if (_score <= MaxScore) return;
            _repository.SaveBestScore(_score);
        }
    }
}