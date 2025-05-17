using UnityEngine;

namespace Game.Engine
{
    public class SmoothFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private Vector3 offset;
        [SerializeField] private float followSpeed;
        [SerializeField] private float rotSpeed;

        private void LateUpdate()
        {
            Vector3 toPosition = target.TransformPoint(offset);
            // toPosition = target.position + offset;
            transform.position = toPosition;
            
            Vector3 targetRot = target.rotation.eulerAngles;
            float xRotation = transform.rotation.eulerAngles.x;
            transform.rotation = Quaternion.Euler(xRotation, targetRot.y, 0);
        }
    }
}
