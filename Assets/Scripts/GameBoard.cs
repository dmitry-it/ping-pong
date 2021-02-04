using System;
using Popups;
using ScoreSystem;
using TMPro;
using UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoard : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject racketPrefab;

    [SerializeField] private PopupFabric popupFabric;
    [SerializeField] private Transform gamePool;
    [SerializeField] private Transform playerZone;
    [SerializeField] private Transform opponentZone;
    [SerializeField] private int delayToStartRound = 3;
    [SerializeField] private TextMeshProUGUI maxScore;
    [SerializeField] private CounterUI scoreUi;

    private Ball _ball;
    private GameMode _gameMode;

    private ScoreCounter _scoreCounter;

    private const string StartLevelPopupName = "StartLevelPopup";
    private const string SettingsPopupName = "SettingsPopup";
    private const string BallPrefabsPath = "Prefabs/Balls";

    private void Start()
    {
        OpenMenu();
    }

    public void StartNewRound(GameMode mode)
    {
        _gameMode = mode;
        LoadLevel();
        _ball.Punch(delayToStartRound);
    }


    private void LoadLevel()
    {
        PrepareCounters();

        _ball = SpawnBall();

        var playerRacket = SpawnRacket(playerZone);
        playerRacket.InitAsPlayerRacket();
        var opponentRacket = SpawnRacket(opponentZone);

        switch (_gameMode)
        {
            case GameMode.SinglePlayer:
                opponentRacket.InitAsSyncedRacket(playerRacket.transform);
                break;
            case GameMode.TwoPlayerOnDevice:
                opponentRacket.InitAsPlayerRacket();
                break;
            case GameMode.NetworkGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PrepareCounters()
    {
        _scoreCounter = new ScoreCounter(new ScoreRepositoryToPlayerPrefs());
        _scoreCounter.ScoreUpdateEvent.AddListener(scoreUi.UpdateCounterValue);
        scoreUi.UpdateCounterValue(0);
        maxScore.text = _scoreCounter.MaxScore.ToString();
    }


    private Ball SpawnBall()
    {
        foreach (var existBall in FindObjectsOfType<Ball>()) Destroy(existBall.gameObject);

        var go = Instantiate(GetRandomBallPrefab(), gamePool, false);
        var ball = go.GetComponent<Ball>();
        ball.contactWithFinishZoneEvent.AddListener(FinishRound);
        ball.CountableSurfacesContactEvent.AddListener(_scoreCounter.AddPointsToScore);
        return ball;
    }

    private Racket SpawnRacket(Transform parentTransform)
    {
        foreach (var racket in parentTransform.GetComponentsInChildren<Racket>()) Destroy(racket.gameObject);

        var go = Instantiate(racketPrefab, parentTransform, false);
        return go.GetComponent<Racket>();
    }

    private void FinishRound()
    {
        _scoreCounter.SaveResult();
        StartNewRound(_gameMode);
    }

    public void OpenMenu()
    {
        popupFabric.OpenPopup<StartLevelPopup>(StartLevelPopupName, popup => { popup.SetGameBoard(this); });
    }

    public void OpenSettings()
    {
        popupFabric.OpenPopup<SettingsPopup>(SettingsPopupName,
            popup => { popup.OnCloseEvent.AddListener(() => StartNewRound(_gameMode)); });
    }

    private GameObject GetRandomBallPrefab()
    {
        var prefabs = Resources.LoadAll<GameObject>(BallPrefabsPath);
        var prefabsCount = prefabs.Length;
        return prefabsCount > 1 ? prefabs[Random.Range(0, prefabsCount - 1)] : ballPrefab;
    }
}