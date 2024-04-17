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

        [Header("Sound")]
        [SerializeField] private Image _soundOn;
        [SerializeField] private Image _soundOff;
        [SerializeField] private TextMeshProUGUI _soundActiveText;

        [Header("Music")]
        [SerializeField] private Image _musicOn;
        [SerializeField] private Image _musicOff;
        [SerializeField] private TextMeshProUGUI _musicActiveText;

        [Header("Score")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _totalScore;
        [SerializeField] private TextMeshProUGUI _attempts;

        [SerializeField] private ThrowingBall _thrownBall;
        [SerializeField] private BallShooter _ballShooter;
        [SerializeField] private Button _shootButton;


        private void OnEnable()
        {
            GameManager.Instance.OnGameOver += ShowGameOverScreen;
            GameManager.Instance.OnPause += TogglePauseMenu;
            GameManager.Instance.OnSwitchMusic += SwitchMusicIcon;
            GameManager.Instance.OnSwitchSound += SwitchSoundIcon;
            GameManager.Instance.OnAddScore += UpdateScoreText;

        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameOver -= ShowGameOverScreen;
            GameManager.Instance.OnPause -= TogglePauseMenu;
            GameManager.Instance.OnSwitchMusic -= SwitchMusicIcon;
            GameManager.Instance.OnSwitchSound -= SwitchSoundIcon;
            GameManager.Instance.OnAddScore -= UpdateScoreText;

            _ballShooter.OnBallShooted -= () => UpdateInteractableButton(false);
            _thrownBall.OnBallNMatched -= UpdateAttemptsText;
        }

        private void Start()
        {
            _thrownBall.OnBallNMatched += UpdateAttemptsText;
            _ballShooter.OnBallShooted += () => UpdateInteractableButton(false);
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
            _gameOver.gameObject.SetActive(true);
            _totalScore.text = GameManager.Instance.CurrentScore.ToString();
        }

        public void ToggleSettings()
        {
            _settingsCanvas.gameObject.SetActive(!_settingsCanvas.gameObject.activeSelf);
        }

        private void TogglePauseMenu(bool active)
        {
            _settingsCanvas.gameObject.SetActive(active);
        }

        private void SwitchMusicIcon()
        {
            _musicOn.gameObject.SetActive(!_musicOn.gameObject.activeSelf);
            _musicOff.gameObject.SetActive(!_musicOff.gameObject.activeSelf);

            if (_musicOn.gameObject.activeSelf)
            {
                _musicActiveText.text = "MUSIC ON";
            }

            else
            {
                _musicActiveText.text = "MUSIC OFF";
            }
        }

        private void SwitchSoundIcon()
        {
            _soundOn.gameObject.SetActive(!_soundOn.gameObject.activeSelf);
            _soundOff.gameObject.SetActive(!_soundOff.gameObject.activeSelf);

            if (_soundOn.gameObject.activeSelf)
            {
                _soundActiveText.text = "SOUND ON";
            }

            else
            {
                _soundActiveText.text = "SOUND OFF";
            }
        }
    }
}