using UnityEngine;

namespace Game.Engine
{
    public class HeroMovement : MonoBehaviour
    {
        public float moveSpeed;
        public float turnSpeed;
        public float currentAngle;
        public float angle = 25f;

        #region CharacterController

        [SerializeField] private CharacterController controller;
        [SerializeField] private float jumpHeight = 2.0f;
        private readonly float _gravity = -9.81f;
        private Vector3 _velocity;
        private bool _isGrounded;

        #endregion

        private void Update()
        {
            _isGrounded = controller.isGrounded;
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = 0f;
            }


            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            Vector3 direction = transform.TransformDirection(new Vector3(0, 0, verticalAxis).normalized);
            //transform.position += direction * Time.deltaTime * moveSpeed;

            HandleRotation(horizontalAxis);
            HandleJump();

            _velocity.y += _gravity * Time.deltaTime;

            Vector3 finalMove = (direction * moveSpeed) + (_velocity.y * Vector3.up);
            controller.Move(finalMove * Time.deltaTime);
        }

        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * _gravity);
            }
        }

        private void HandleRotation(float steerInput)
        {
            currentAngle = steerInput * angle;

            Vector3 euler = transform.rotation.eulerAngles;
            euler += new Vector3(0, currentAngle, 0) * Time.deltaTime * turnSpeed;
            transform.rotation = Quaternion.Euler(euler);
        }
    }
}