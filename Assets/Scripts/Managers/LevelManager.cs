using System;
using UnityEngine;

namespace ElbowGames.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        [SerializeField] private int LevelGoal = 5;

        public event Action OnLevelPassed;

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

            GameManager.Instance.OnAddScore += CheckLevelPassed;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnAddScore -= CheckLevelPassed;
        }

        private void CheckLevelPassed()
        {
            if (GameManager.Instance.CurrentScore == LevelGoal)
            {
                OnLevelPassed?.Invoke();
            }
        }
    }

}