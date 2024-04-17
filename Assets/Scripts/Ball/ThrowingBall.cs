using ElbowGames.Ball;
using System;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using ElbowGames.Managers;
using System.Collections;

public class ThrowingBall : MonoBehaviour
{
    [field: SerializeField] public BallColor Color { get; private set; }

    [SerializeField] private List<Sprite> _ballSprites = new List<Sprite>();
    [SerializeField] private Transform _way;

    [Header("Sounds")]
    [SerializeField] private AudioClip _matchedSound;
    [SerializeField] private AudioClip _wrongSound;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Vector3 _initialPosition;
    
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

        OnBallMatched += () => SoundManager.Instance.PlaySfx(_matchedSound);
        OnBallNMatched += () => SoundManager.Instance.PlaySfx(_wrongSound);
        OnBallNMatched += SetWayActive;
    }

    private void OnDisable()
    {
        OnBallMatched -= () => SoundManager.Instance.PlaySfx(_matchedSound);
        OnBallNMatched -= () => SoundManager.Instance.PlaySfx(_wrongSound);
        OnBallNMatched -= SetWayActive;
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

    private void MatchBall(Ball ball)
    {
        transform.DOMove(ball.transform.position, 0.25f).OnComplete(() =>
        {
            _way.localPosition = Vector3.zero;
            transform.localPosition = Vector3.zero;

            ChangeBall();
            StartCoroutine(nameof(SetWayActiveCoroutine));
            OnBallMatched?.Invoke();
        });
    }

    private void SetWayActive()
    {
        transform.localPosition = Vector3.zero;
        _way.gameObject.SetActive(true);
    }

    private IEnumerator SetWayActiveCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        SetWayActive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rb.velocity = Vector3.zero;
        _way.localPosition = Vector3.zero;

        if (collision.TryGetComponent(out Ball ball))
        {
            if (ball.Color == Color)
            {
                MatchBall(ball);
            }

            else
            {
                OnBallNMatched?.Invoke();
            }
        }

        else
        {
            OnBallNMatched?.Invoke();
        }
    }
}
