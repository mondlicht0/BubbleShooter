using System;
using UnityEngine;

namespace ElbowGames.Ball
{
    public class BallShooter : MonoBehaviour
    {
        [SerializeField] private ThrowingBall _throwingBall;
        [SerializeField] private Transform _way;
        [SerializeField] private float _shootForce;

        private Rigidbody2D _ballRb;
        public event Action OnBallShooted;

        private void Awake()
        {
            _ballRb = _throwingBall.GetComponent<Rigidbody2D>();
        }

        public void LaunchBall()
        {

            if (_ballRb)
            {
                _way.gameObject.SetActive(false);
                _ballRb.AddForce(_way.up * _shootForce, ForceMode2D.Impulse);
                OnBallShooted.Invoke();
            }
        }
    }
}
