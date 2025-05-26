using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Engine
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private Vector3 offset;
        [SerializeField] private Vector3 topCameraOffset;
        [SerializeField] private float followSpeed;
        [SerializeField] private float rotSpeed;

        [SerializeField] private Transform dollyCamera;

        private float _distanceFromTarget;
        private Vector3 _normalisedDirection;

        [SerializeField] private float minDistance;

        [SerializeField] private bool isTopDown;


        private void Awake()
        {
            if (isTopDown)
            {
                transform.rotation = Quaternion.Euler(90, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(28.8f, 0, 0);
            }
        }


        private void Update()
        {
            if (isTopDown)
            {
                return;
            }

            Vector3 toPosition = target.TransformPoint(offset);
            _normalisedDirection = (toPosition - target.position).normalized;

            if (Physics.Linecast(target.position, transform.position, out RaycastHit hit) &&
                !hit.transform.name.Equals("Hero"))
            {
                _distanceFromTarget = Mathf.Clamp(hit.distance * 0.65f, minDistance, 15f);
            }
            else
            {
                _distanceFromTarget = Vector3.Distance(toPosition, target.position);
            }

            Vector3 finalPosition = target.position + (_normalisedDirection * _distanceFromTarget);
            dollyCamera.position = Vector3.Lerp(dollyCamera.position, finalPosition, Time.deltaTime * 5f);
        }

        private void LateUpdate()
        {
            Vector3 currentOffset = isTopDown ? topCameraOffset : offset;
            Vector3 toPosition = target.TransformPoint(currentOffset);
            transform.position = toPosition;

            Vector3 targetRot = target.rotation.eulerAngles;
            float xRotation = transform.rotation.eulerAngles.x;
            transform.rotation = isTopDown ? Quaternion.Euler(90, 0, 0) : Quaternion.Euler(xRotation, targetRot.y, 0);
        }

        public void ChangeCameraOrientation(bool changeOrientation)
        {
            isTopDown = changeOrientation;

            if (!isTopDown)
            {
                transform.rotation = Quaternion.Euler(28.8f, 0, 0);
            }
        }
    }
}