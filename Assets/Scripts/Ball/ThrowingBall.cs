using ElbowGames.Ball;
using System;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using ElbowGames.Managers;

public class ThrowingBall : MonoBehaviour
{

    [SerializeField] private List<Sprite> _ballSprites = new List<Sprite>();
    [SerializeField] private Transform _way;

    private SpriteRenderer _spriteRenderer;
    private Vector3 _initialPosition;
    private Rigidbody2D _rb;

    [field: SerializeField] public BallColor Color { get; private set; }
    public event Action OnBallCollided;
    public event Action OnBallMatched;
    public event Action OnBallNMatched;

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody2D>();
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void ChangeBall()
    {
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                Color = BallColor.RED;
                break;
            case 1:
                Color = BallColor.YELLOW;
                break;
            case 2:
                Color = BallColor.BLUE;
                break;
            case 3:
                Color = BallColor.GREEN;
                break;
        }
        _spriteRenderer.sprite = _ballSprites[(int)Color];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ball ball))
        {
            if (ball.Color == Color)
            {
                transform.DOMove(ball.transform.position, 0.25f).OnComplete(() =>
                {
                    _rb.velocity = Vector3.zero;
                    transform.localPosition = Vector3.zero;
                    _way.localPosition = Vector3.zero;
                    OnBallMatched?.Invoke();
                    ChangeBall();
                });
            }

            else
            {
                _rb.velocity = Vector3.zero;
                transform.localPosition = Vector3.zero;
                _way.localPosition = Vector3.zero;
                OnBallNMatched?.Invoke();
            }
        }

        else
        {
            _rb.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
            _way.localPosition = Vector3.zero;
            OnBallNMatched?.Invoke();
        }

        OnBallCollided?.Invoke();
    }
}
