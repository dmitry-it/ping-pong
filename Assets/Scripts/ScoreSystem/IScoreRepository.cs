namespace Assets.Scripts.ScoreSystem
{
    public interface IScoreRepository
    {
        void SaveBestScore(int score);
        int LoadBestScore();
    }
}