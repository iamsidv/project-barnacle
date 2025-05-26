using System.Collections.Generic;
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
        [SerializeField] public float indoorMoveSpeed = 3f;
        [SerializeField] public float indoorTurnSpeed = 10f;
        
        [SerializeField] private CharacterController controller;
        [SerializeField] public float jumpHeight = 2.0f;
        [SerializeField] private float downDistance = 0.15f;

        private bool _isGrounded;
        private Vector3 _velocity;
        private IMovementModule _movement;

        private Dictionary<string, IMovementModule> _movementFactory;

        private readonly string _indoorMovementKey = "indoor";
        private readonly string _outdoorMovementKey = "outdoor";

        [SerializeField] private HeroAnimatorController animatorController;
        
        private void Start()
        {
            _movementFactory = new Dictionary<string, IMovementModule>()
            {
                { _outdoorMovementKey, new OutdoorTraversalMovement(this) },
                { _indoorMovementKey, new IndoorMovement(this) }
            };
            
            SetMovement(_outdoorMovementKey);

            animatorController = transform.GetComponentInChildren<HeroAnimatorController>();
        }

        private void SetMovement(string id)
        {
            _movement = _movementFactory[id];
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

            animatorController.LogData(moveDirection, controller.velocity, _velocity.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"TriggerEnter {other.gameObject.name}");

            if (other.TryGetComponent(out InteractableView view))
            {
                view.ShowHint();
            }

            if (other.CompareTag("HouseEnter"))// || other.CompareTag("HouseExit") )
            {
                Debug.Log("_debug_ HouseEnter TriggerEnter");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"TriggerExit {other.gameObject.name}");

            if (other.TryGetComponent(out InteractableView view))
            {
                view.HideHint();
            }
            
            if (other.CompareTag("HouseEnter"))
            {
                Debug.Log("_debug_ HouseEnter TriggerExit");
                
                GameManager.Instance.EnterHouse();
                SetMovement(_indoorMovementKey);
            }
            
            if (other.CompareTag("HouseExit"))
            {
                Debug.Log("_debug_ HouseExit TriggerExit");
                
                GameManager.Instance.ExitHouse();
                SetMovement(_outdoorMovementKey);
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