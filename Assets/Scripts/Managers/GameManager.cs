using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElbowGames.Managers
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private ThrowingBall _ball;
        public int CurrentScore { get; private set; }
        public bool IsGameOver { get; private set; } = false;
        [field: SerializeField] public int Attempts { get; private set; } = 5;

        private bool _isPause = false;

        public event Action OnGameOver;
        public event Action OnAddScore;
        public event Action OnSwitchSound;
        public event Action OnSwitchMusic;
        public event Action<bool> OnPause;

        private void OnEnable()
        {
            OnGameOver += StopGame;
            OnPause += PauseGame;
        }

        private void OnDisable()
        {
            LevelManager.Instance.OnLevelPassed -= StopGame;
            OnPause -= PauseGame;
            _ball.OnBallMatched -= AddScore;
            _ball.OnBallNMatched -= ReduceAttempts;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(Instance);
                Instance = this;
            }
        }

        private void Start()
        {
            LevelManager.Instance.OnLevelPassed += StopGame;

            _ball.OnBallMatched += AddScore;
            _ball.OnBallNMatched += ReduceAttempts;

            Time.timeScale = 1;
        }

        private void ReduceAttempts()
        {
            Attempts--;
            if (Attempts < 1)
            {
                OnGameOver?.Invoke();
            }
        }

        private void StopGame()
        {
            Time.timeScale = 0;
        }

        private void AddScore()
        {
            CurrentScore += 1;
            OnAddScore?.Invoke();
        }

        public void Pause()
        {
            _isPause = !_isPause;
            OnPause?.Invoke(_isPause);
        }

        public void Retry()
        {
            SceneManager.LoadScene("Loading");
        }

        public void SwitchMusic()
        {
            OnSwitchMusic?.Invoke();
        }

        public void SwitchSound()
        {
            OnSwitchSound?.Invoke();
        }

        private void PauseGame(bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
        }

    }
}
