using UnityEngine;

namespace _Project._Code.Features.Vehicle
{
    public class WheelVisual : MonoBehaviour
    {
        [SerializeField] private Rigidbody _mainRigidbody;
        [SerializeField] private float _wheelRadius;
        [SerializeField] private Vector3 _spinAxis = Vector3.right;

        private void Update()
        {
            Vector3 pointVelocity = _mainRigidbody.GetPointVelocity(transform.position);
            float forwardSpeed = Vector3.Dot(pointVelocity, _mainRigidbody.transform.forward);
            float angularSpeed = forwardSpeed / _wheelRadius;
            float degreesThisFrame = angularSpeed * Mathf.Rad2Deg * Time.deltaTime;
            
            transform.Rotate(_spinAxis * degreesThisFrame, Space.Self);
        }
    }
}