using System;
using UnityEngine;

namespace Game.Engine
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private float turnSpeed = 70f;

        [SerializeField] private Transform heroTransform;
        [SerializeField] private Transform pointerTransform;

        public Vector3 actual;

        private void Start()
        {
            heroTransform = transform;
            pointerTransform.position = heroTransform.position;
        }

        private void Update()
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(horizontalAxis, 0, verticalAxis);
            transform.Rotate(new Vector3(0,horizontalAxis,0) * Time.deltaTime * turnSpeed);
            transform.Translate(Vector3.forward * verticalAxis * Time.deltaTime * moveSpeed);
        }
    }
}