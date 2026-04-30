using Unity.Cinemachine;
using UnityEngine;

namespace _Project._Code.Features.FollowCamera
{
    public class CinemachineFollowCamera : MonoBehaviour, IFollowCamera
    {
        private CinemachineInputAxisController _inputAxisController;
        private CinemachineCamera _cinemachineCamera;

        private void Awake()
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
            _inputAxisController = GetComponent<CinemachineInputAxisController>();
        }
        
        public void SetTarget(Transform target)
        {
            _cinemachineCamera.Follow = target;
            _cinemachineCamera.LookAt = target;
        }
    }
}