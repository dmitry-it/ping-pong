using System;
using Assets.Scripts.Popups;
using Assets.Scripts.ScoreSystem;
using Assets.Scripts.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
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
        private GameMode gameMode;
        private Ball _ball;

        private ScoreCounter _scoreCounter;

        public UnityEvent OnRoundEndEvent;

        public void StartNewRound(GameMode mode)
        {
            gameMode = mode;
            LoadLevel();
            _ball.Punch(delayToStartRound);
        }

        private void Start()
        {
            OpenMenu();
        }


        private void LoadLevel()
        {
            OnRoundEndEvent = new UnityEvent();

            PrepareCounters();

            _ball = SpawnBall();

            var playerRacket = SpawnRacket(playerZone);
            playerRacket.InitAsPlayerRacket();
            var opponentRacket = SpawnRacket(opponentZone);

            switch (gameMode)
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
            _scoreCounter.OnScoreUpdateEvent.AddListener(scoreUi.UpdateCounterValue);
            scoreUi.UpdateCounterValue(0);
            maxScore.text = _scoreCounter.MaxScore.ToString();
        }


        private Ball SpawnBall()
        {
            foreach (var existBall in FindObjectsOfType<Ball>())
            {
                Destroy(existBall.gameObject);
            }
            
            var go = Instantiate(GetRandomBallPrefab(), gamePool, false);
            var ball = go.GetComponent<Ball>();
            ball.OnContactWithFinishZone.AddListener(FinishRound);
            ball.onCountableSurfacesContactEvent.AddListener(_scoreCounter.AddPointsToScore);
            return ball;
        }

        private Racket SpawnRacket(Transform parentTransform)
        {
            foreach (var racket in parentTransform.GetComponentsInChildren<Racket>())
            {
                Destroy(racket.gameObject);
            }

            var go = Instantiate(racketPrefab, parentTransform, false);
            return go.GetComponent<Racket>();
        }


        private void FinishRound()
        {
            _scoreCounter.SaveResult();
            StartNewRound(gameMode);
        }

        public void OpenMenu()
        {
            popupFabric.OpenPopup<StartLevelPopup>("StartLevelPopup", popup => { popup.SetGameBoard(this); });
        }

        public void OpenSettings()
        {
            popupFabric.OpenPopup<SettingsPopup>("SettingsPopup",
                popup =>
                {
                    popup.OnCloseEvent.AddListener(() => StartNewRound(gameMode));
                });
        }

        private GameObject GetRandomBallPrefab()
        {
            var prefabs = Resources.LoadAll<GameObject>("Prefabs/Balls");
            var prefabsCount = prefabs.Length;
            return prefabsCount > 1 ? prefabs[Random.Range(0, prefabsCount - 1)] : ballPrefab;
        }
    }
}