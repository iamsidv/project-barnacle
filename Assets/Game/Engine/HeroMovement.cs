using UnityEngine;

namespace Game.Engine
{
    public class HeroMovement : MonoBehaviour
    {
        private readonly float _gravity = -9.81f;

        public float moveSpeed;
        public float turnSpeed;
        public float currentAngle;
        public float angle = 25f;
        public float climbSpeed = 2f;

        [SerializeField] private CharacterController controller;
        [SerializeField] private float jumpHeight = 2.0f;
        private bool _isGrounded;
        private bool _isClimbing;
        [SerializeField] private Transform climbTarget;

        private float TraversalSpeed => _isClimbing ? climbSpeed : moveSpeed;

        private Vector3 _velocity;
        [SerializeField] private float downDistance = 0.15f;
        [SerializeField] private float frontDistance = 0.3f;
        
        private void Update()
        {
            Vector3 rayStartPoint = transform.TransformPoint(Vector3.down);
            Vector3 rayDirection = Vector3.down * downDistance;
            Debug.DrawRay(rayStartPoint, rayDirection, Color.chartreuse);

            _isGrounded = Physics.Raycast(rayStartPoint, rayDirection, out RaycastHit _, downDistance);
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = 0f;
            }

            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            Vector3 direction = transform.TransformDirection(new Vector3(0, 0, verticalAxis).normalized);
            //transform.position += direction * Time.deltaTime * moveSpeed;

            Vector3 topPoint = transform.TransformPoint(new Vector3(0, 0.7f, 0.5f));
            Vector3 bottomPoint = transform.TransformPoint(new Vector3(0, -0.7f, 0.5f));
            bool isTouchingTop = Physics.Raycast(topPoint, transform.forward, out RaycastHit top, frontDistance);
            bool isTouchingBottom = Physics.Raycast(bottomPoint, transform.forward, out RaycastHit bottom, frontDistance);


            if (isTouchingTop && top.transform.CompareTag(TagHandle.GetExistingTag("Climbable")) &&
                Vector3.Angle(transform.forward, top.transform.forward) <= 15f)
            {
                if (verticalAxis > 0)
                {
                    Vector3 pos = top.transform.position;
                    pos.y = transform.position.y;
                    pos.x = top.transform.position.x;
                    transform.position = pos;
                    transform.rotation = top.transform.rotation;
                    _isClimbing = true;
                    climbTarget = top.transform;
                }
                else
                {
                    if (_isGrounded)
                    {
                        climbTarget = null;
                        _isClimbing = false;
                        Vector3 pos = top.transform.TransformPoint(0, 0, -1f);
                        pos.y = transform.position.y;
                        pos.x = top.transform.position.x;
                        transform.position = pos;
                    }
                }
            }

            if (climbTarget && !isTouchingTop && !isTouchingBottom)
            {
                climbTarget = null;
                _isClimbing = false;
                direction = transform.TransformDirection(Vector3.forward);
            }

            Debug.DrawRay(topPoint, Vector3.forward * downDistance, Color.brown);
            Debug.DrawRay(bottomPoint, Vector3.forward * downDistance, Color.brown);


            if (_isClimbing)
            {
                direction = transform.TransformDirection(new Vector3(0, verticalAxis, 0).normalized);
                _velocity.y = 0f;
                horizontalAxis = 0f;
            }

            HandleRotation(horizontalAxis);
            HandleJump();

            _velocity.y += _gravity * Time.deltaTime;

            Vector3 finalMove = (direction * TraversalSpeed) + (_velocity.y * Vector3.up);
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