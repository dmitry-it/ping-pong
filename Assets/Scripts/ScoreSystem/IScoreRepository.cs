namespace ScoreSystem
{
    public interface IScoreRepository
    {
        void SaveBestScore(int score);
        int LoadBestScore();
    }
}