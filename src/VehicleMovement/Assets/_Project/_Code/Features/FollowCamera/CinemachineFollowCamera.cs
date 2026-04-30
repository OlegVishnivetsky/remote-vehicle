using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

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

        // private void Update()
        // {
        //     bool pointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        //     _inputAxisController.enabled = !pointerOverUI;
        // }

        public void SetTarget(Transform target)
        {
            _cinemachineCamera.Follow = target;
            _cinemachineCamera.LookAt = target;
        }
    }
}