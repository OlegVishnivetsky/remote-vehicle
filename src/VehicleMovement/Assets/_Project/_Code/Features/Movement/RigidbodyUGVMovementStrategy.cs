using System.Collections.Generic;
using _Project._Code.Features.Configs.VehicleMovement;
using _Project._Code.Features.Vehicle;
using UnityEngine;

namespace _Project._Code.Features.Movement
{
    public class RigidbodyUGVMovementStrategy : IVehicleMovementStrategy
    {
        private readonly Rigidbody _rigidbody;
        private readonly VehicleMovementConfig _movementConfig;
        
        private Transform _rootTransform;
        private List<Transform> _wheels;

        private float _throttle;
        private float _steering;
        
        private bool[] _wheelsOnGroundStatuses;
        private RaycastHit[] _wheelHits;
        
        public RigidbodyUGVMovementStrategy(Rigidbody rigidbody, VehicleMovementConfig movementConfig)
        {
            _rigidbody = rigidbody;
            _movementConfig = movementConfig;
        }

        public void Initialize(VehicleContext context)
        {
            _wheels = context.Wheels;
            _rootTransform = context.Root; 
            _wheelsOnGroundStatuses = new bool[_wheels.Count];
            _wheelHits = new RaycastHit[_wheels.Count];
            
            _rigidbody.mass = _movementConfig.Mass;
            _rigidbody.linearDamping = _movementConfig.LinearDamping;
            _rigidbody.centerOfMass = _movementConfig.CenterOfMass;
        }

        public void SimulateValues(Vector2 input)
        {
            _throttle = input.y;
            _steering = input.x;
        }

        public void SimulatePhysics()
        {
            for (int wheelIndex = 0; wheelIndex < _wheels.Count; wheelIndex++)
            {
                Transform wheel = _wheels[wheelIndex];
                
                GetWheelHitInformation(wheel.position, wheelIndex, out RaycastHit hitInfo);

                if (!_wheelsOnGroundStatuses[wheelIndex])
                    continue;

                Vector3 pointVelocity = _rigidbody.GetPointVelocity(wheel.position);

                ApplySpringForce(hitInfo, pointVelocity, wheel.position);
                ApplyMainForce(wheel, hitInfo);
                ApplyFrictionForce(pointVelocity, hitInfo);
            }
        }
        
        public void DrawDebugGizmos()
        {
            if (_wheels == null || _movementConfig == null) return;
    
            for (int i = 0; i < _wheels.Count; i++)
            {
                Transform wheel = _wheels[i];
                if (wheel == null) continue;

                Vector3 origin = wheel.position + _rootTransform.up * _movementConfig.WheelRadius;
                Vector3 maxReach = wheel.position - _rootTransform.up * _movementConfig.SuspensionTravel;

                Gizmos.color = Color.gray;
                Gizmos.DrawLine(origin, maxReach);

                Gizmos.color = _wheelsOnGroundStatuses[i] ? Color.green : Color.red;
                Gizmos.DrawWireSphere(wheel.position, _movementConfig.WheelRadius);

                if (_wheelsOnGroundStatuses[i])
                {
                    RaycastHit hit = _wheelHits[i];
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(hit.point, 0.04f);
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(hit.point, hit.point + hit.normal * 0.3f);
                }
            }

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(_rigidbody.worldCenterOfMass, 0.05f);
        }
        
        private void GetWheelHitInformation(Vector3 wheelPosition, int wheelIndex, out RaycastHit hitInfo)
        {
            Vector3 origin = wheelPosition + _rootTransform.up * _movementConfig.WheelRadius;
            
            _wheelsOnGroundStatuses[wheelIndex] = Physics.SphereCast(
                origin, 
                _movementConfig.WheelRadius, 
                -_rootTransform.up, 
                out hitInfo,
                _movementConfig.SuspensionTravel,
                _movementConfig.GroundLayer);
            
            _wheelHits[wheelIndex] = hitInfo;
        }

        private void ApplySpringForce(RaycastHit hitInfo, Vector3 pointVelocity, Vector3 wheelPosition)
        {
            float compression = _movementConfig.SuspensionTravel - hitInfo.distance;
            compression = Mathf.Max(0f, compression);
    
            float pointVelocityAlongNormal = Vector3.Dot(pointVelocity, hitInfo.normal);
            float springForce = compression * _movementConfig.SpringStiffness 
                                - pointVelocityAlongNormal * _movementConfig.DampingCoefficient;
    
            _rigidbody.AddForceAtPosition(hitInfo.normal * springForce, wheelPosition);
        }

        private void ApplyMainForce(Transform wheel, RaycastHit hitInfo)
        {
            bool isLeft = wheel.localPosition.x < 0f;
            Vector3 driveDirection = Vector3.ProjectOnPlane(_rootTransform.forward, hitInfo.normal).normalized;
                
            float forwardComponent = _throttle * _movementConfig.MotorForce;
            float steeringComponent = _steering * _movementConfig.SteeringForce;
                
            float leftThrust = forwardComponent + steeringComponent;
            float rightThrust = forwardComponent - steeringComponent;
            float wheelThrust = isLeft ? leftThrust : rightThrust;
                
            Vector3 finalForce = driveDirection * wheelThrust;
            
            _rigidbody.AddForceAtPosition(finalForce, hitInfo.point);
        }
        
        private void ApplyFrictionForce(Vector3 pointVelocity, RaycastHit hitInfo)
        {
            float sidewaysVelocity = Vector3.Dot(pointVelocity, _rootTransform.right);
            Vector3 frictionForce = -_rootTransform.right * (sidewaysVelocity * _movementConfig.GripCoefficient);
            _rigidbody.AddForceAtPosition(frictionForce, hitInfo.point);
        }
    }
}