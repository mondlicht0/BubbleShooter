using UnityEngine;

namespace ElbowGames.Ball
{
    public class OrbitAround : MonoBehaviour
    {
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _maxAngle = 360f;

        private bool rotateClockwise = true;
        private void Update()
        {

            if (Vector3.Angle(transform.up, _centerPoint.transform.up) > _maxAngle)
            {
                rotateClockwise = !rotateClockwise;
            }

            if (rotateClockwise)
            {
                transform.RotateAround(_centerPoint.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
            }

            else
            {
                transform.RotateAround(_centerPoint.position, Vector3.back, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}
