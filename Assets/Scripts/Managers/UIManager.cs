using ElbowGames.Ball;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElbowGames.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Main Menu")]
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private Canvas _gameOver;
        [SerializeField] private Canvas _victory;
        [SerializeField] private Canvas _settingsCanvas;

        [Header("Score")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _totalScoreWin;
        [SerializeField] private TextMeshProUGUI _totalScoreOver;
        [SerializeField] private TextMeshProUGUI _attempts;

        [SerializeField] private ThrowingBall _thrownBall;
        [SerializeField] private BallShooter _ballShooter;
        [SerializeField] private Button _shootButton;


        private void OnEnable()
        {
            GameManager.Instance.OnGameOver += ShowGameOverScreen;
            GameManager.Instance.OnPause += TogglePauseMenu;
            GameManager.Instance.OnAddScore += UpdateScoreText;

            LevelManager.Instance.OnLevelPassed += ShowVictoryScreen;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameOver -= ShowGameOverScreen;
            GameManager.Instance.OnPause -= TogglePauseMenu;
            GameManager.Instance.OnAddScore -= UpdateScoreText;

            LevelManager.Instance.OnLevelPassed -= ShowVictoryScreen;

            _ballShooter.OnBallShooted -= () => UpdateInteractableButton(false);
            _thrownBall.OnBallMatched -= () => UpdateInteractableButton(true);
            _thrownBall.OnBallNMatched -= () => UpdateInteractableButton(true);
            _thrownBall.OnBallNMatched -= UpdateAttemptsText;
        }

        private void Start()
        {
            _thrownBall.OnBallNMatched += UpdateAttemptsText;
            _ballShooter.OnBallShooted += () => UpdateInteractableButton(false);
            _thrownBall.OnBallMatched += () => UpdateInteractableButton(true);
            _thrownBall.OnBallNMatched += () => UpdateInteractableButton(true);
            UpdateAttemptsText();
        }

        private void UpdateInteractableButton(bool active)
        {
            _shootButton.interactable = active;
        }

        private void UpdateAttemptsText()
        {
            _attempts.text = GameManager.Instance.Attempts.ToString();
        }

        private void UpdateScoreText()
        {
            _scoreText.text = GameManager.Instance.CurrentScore.ToString();
        }

        private void ShowGameOverScreen()
        {
            _totalScoreOver.text = GameManager.Instance.CurrentScore.ToString();
            UpdateInteractableButton(false);
            _gameOver.gameObject.SetActive(true);

        }

        private void ShowVictoryScreen()
        {
            _totalScoreWin.text = GameManager.Instance.CurrentScore.ToString();
            UpdateInteractableButton(false);
            _victory.gameObject.SetActive(true);
        }

        private void TogglePauseMenu(bool active)
        {
            _settingsCanvas.gameObject.SetActive(active);
        }
    }
}