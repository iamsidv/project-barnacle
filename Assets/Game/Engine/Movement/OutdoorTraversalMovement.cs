using UnityEngine;

namespace Game.Engine.Movement
{
    public class OutdoorTraversalMovement : IMovementModule
    {
        private readonly float _downDistance = 0.15f;
        private readonly float _frontDistance = 0.3f;
        
        private readonly Transform _transform;
        private float _verticalAxis;
        private float _horizontalAxis;
        
        private readonly HeroMovement _owner;
        private Transform _climbTarget;
        private float TraversalSpeed => _isClimbing ? _owner.climbSpeed : _owner.moveSpeed;
        private bool _isClimbing;

        private Vector3 MoveDirection { get; set; }
        
        public OutdoorTraversalMovement(HeroMovement owner)
        {
            _owner = owner;
            _transform = owner.transform;
        }

        public void SetInputAxis(float horizontal, float vertical)
        {
            _verticalAxis = vertical;
            _horizontalAxis = horizontal;
        }

        public void TickMovement()
        {
            Vector3 direction = _transform.TransformDirection(new Vector3(0, 0, _verticalAxis).normalized);
            Vector3 topPoint = _transform.TransformPoint(new Vector3(0, 0.7f, 0.5f));
            Vector3 bottomPoint = _transform.TransformPoint(new Vector3(0, -0.7f, 0.5f));
            bool isTouchingTop = Physics.Raycast(topPoint, _transform.forward, out RaycastHit top, _frontDistance);
            bool isTouchingBottom =
                Physics.Raycast(bottomPoint, _transform.forward, out RaycastHit bottom, _frontDistance);

            if (isTouchingTop && top.transform.CompareTag(TagHandle.GetExistingTag("Climbable")) &&
                Vector3.Angle(_transform.forward, top.transform.forward) <= 15f)
            {
                if (_verticalAxis > 0)
                {
                    Vector3 pos = top.transform.position;
                    pos.y = _transform.position.y;
                    pos.x = top.transform.position.x;
                    _transform.position = pos;
                    _transform.rotation = top.transform.rotation;
                    _isClimbing = true;
                    _climbTarget = top.transform;
                }
                else
                {
                    if (_owner.IsGrounded)
                    {
                        _climbTarget = null;
                        _isClimbing = false;
                        Vector3 pos = top.transform.TransformPoint(0, 0, -1f);
                        pos.y = _transform.position.y;
                        pos.x = top.transform.position.x;
                        _transform.position = pos;
                    }
                }
            }

            if (_climbTarget && !isTouchingTop && !isTouchingBottom)
            {
                _climbTarget = null;
                _isClimbing = false;
                direction = _transform.TransformDirection(Vector3.forward);
            }

            Debug.DrawRay(topPoint, Vector3.forward * _downDistance, Color.brown);
            Debug.DrawRay(bottomPoint, Vector3.forward * _downDistance, Color.brown);

            if (_isClimbing)
            {
                direction = _transform.TransformDirection(new Vector3(0, _verticalAxis, 0).normalized);
                // _owner.Velocity.y = 0f;
                _owner.SetVelocity(Vector3.zero);
                _horizontalAxis = 0f;
            }

            HandleRotation(_horizontalAxis);
            HandleJump();


            MoveDirection = (direction * TraversalSpeed) + (_owner.Velocity.y * Vector3.up);
        }

        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && _owner.IsGrounded)
            {
                float yVelocity = Mathf.Sqrt(_owner.jumpHeight * -2.0f * _owner._gravity);
                _owner.SetVelocity(new Vector3(0, yVelocity, 0));
            }
        }

        private void HandleRotation(float steerInput)
        {
            Vector3 euler = _transform.rotation.eulerAngles;
            euler += new Vector3(0, steerInput * _owner.angle, 0) * Time.deltaTime * _owner.turnSpeed;
            _transform.rotation = Quaternion.Euler(euler);
        }

        public Vector3 GetMoveDirection()
        {
            return MoveDirection;
        }
    }
}