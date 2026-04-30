using _Project._Code.Features.Vehicle;
using UnityEngine;

namespace _Project._Code.Features.Configs.VehicleMovement
{
    [CreateAssetMenu(fileName = "Vehicle Config", menuName = "Configs/Vehicle Movement Config")]
    public class VehicleMovementConfig : ScriptableObject
    {
        public VehicleTypeId VehicleType;
        
        [Header("Body")] 
        public int Mass = 30;
        public float LinearDamping = 4f;
        public Vector3 CenterOfMass = new(0, -0.2f, 0);

        [Header("Wheel")] 
        public LayerMask GroundLayer;
        public float WheelRadius = 0.2f;
        public float SuspensionTravel = 0.02f;
        
        [Header("Forces")] 
        public float MotorForce = 100f;
        public float SteeringForce = 20f;
        public float GripCoefficient = 80f;
        public float SpringStiffness = 1400f;
        public float DampingCoefficient = 200f;
    }
}