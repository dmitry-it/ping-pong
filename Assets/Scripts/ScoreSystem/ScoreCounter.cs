namespace ScoreSystem
{
    public class ScoreCounter
    {
        private readonly IScoreRepository _repository;
        public readonly ScoreUpdateEvent ScoreUpdateEvent = new ScoreUpdateEvent();

        public ScoreCounter(IScoreRepository repository)
        {
            _repository = repository;
        }

        public int CurrentScore { get; private set; }

        public int MaxScore => _repository.LoadBestScore();

        public void AddPointsToScore(int points)
        {
            CurrentScore += points;
            ScoreUpdateEvent.Invoke(CurrentScore);
        }

        public void SaveResult()
        {
            if (CurrentScore <= MaxScore) return;
            _repository.SaveBestScore(CurrentScore);
        }
    }
}