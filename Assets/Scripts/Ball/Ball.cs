using UnityEngine;

namespace ElbowGames.Ball
{
    public class Ball : MonoBehaviour
    {
        [field: SerializeField] public BallColor Color { get; private set; }
    }
}

public enum BallColor
{
    RED,
    YELLOW,
    BLUE,
    GREEN,
}
