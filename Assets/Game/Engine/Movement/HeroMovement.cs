using Game.Engine.Interaction;
using UnityEngine;

namespace Game.Engine.Movement
{
    public class HeroMovement : MonoBehaviour
    {
        public readonly float _gravity = -9.81f;

        public float moveSpeed;
        public float turnSpeed;
        public float angle = 25f;
        public float climbSpeed = 2f;

        [SerializeField] private CharacterController controller;
        [SerializeField] public float jumpHeight = 2.0f;
        [SerializeField] private float downDistance = 0.15f;

        private bool _isGrounded;
        private Vector3 _velocity;
        private IMovementModule _movement;

        private void Start()
        {
            _movement = new OutdoorTraversalMovement(this);
        }

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

            _movement.SetInputAxis(horizontalAxis, verticalAxis);
            _movement.TickMovement();

            _velocity.y += _gravity * Time.deltaTime;

            Vector3 moveDirection = _movement.GetMoveDirection();
            controller.Move(moveDirection * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"TriggerEnter {other.gameObject.name}");

            if (other.TryGetComponent(out InteractableView view))
            {
                view.ShowHint();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"TriggerExit {other.gameObject.name}");

            if (other.TryGetComponent(out InteractableView view))
            {
                view.HideHint();
            }
        }

        public bool IsGrounded => _isGrounded;
        public Vector3 Velocity => _velocity;

        public void SetVelocity(Vector3 zero)
        {
            _velocity = zero;
        }
    }
}