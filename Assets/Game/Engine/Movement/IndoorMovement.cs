using UnityEngine;

namespace Game.Engine.Movement
{
    public class IndoorMovement : IMovementModule
    {
        private float _verticalAxis;
        private float _horizontalAxis;
        private readonly HeroMovement _owner;
        private readonly Transform _transform;
        private Vector3 _moveDirection;

        public IndoorMovement(HeroMovement owner)
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
            Vector3 direction = new Vector3(_horizontalAxis, 0, _verticalAxis).normalized;
            _moveDirection = direction * _owner.indoorMoveSpeed + (_owner.Velocity.y * Vector3.up);

            if (direction.magnitude >= 0.1f)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                _transform.rotation =
                    Quaternion.Slerp(_transform.rotation, rotation, Time.deltaTime * _owner.indoorTurnSpeed);
            }
        }

        public Vector3 GetMoveDirection()
        {
            return _moveDirection;
        }
    }
}