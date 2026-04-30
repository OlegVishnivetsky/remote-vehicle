using System.Collections.Generic;
using UnityEngine;

namespace _Project._Code.Features.Vehicle
{
    public class VehicleContext
    {
        public Transform Root;
        public List<Transform> Wheels;
        public Rigidbody Rigidbody;
    }
}