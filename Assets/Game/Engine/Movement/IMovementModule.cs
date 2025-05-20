using UnityEngine;

namespace Game.Engine.Movement
{
    public interface IMovementModule
    {
        void SetInputAxis(float horizontal, float vertical);
        void TickMovement();
        Vector3 GetMoveDirection();
    }
}