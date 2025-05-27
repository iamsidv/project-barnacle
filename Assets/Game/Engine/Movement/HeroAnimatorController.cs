using UnityEngine;

namespace Game.Engine.Movement
{
    public class HeroAnimatorController : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Jump = Animator.StringToHash("Jump");
        
        [SerializeField] private Animator animator;
        
        public void LogData(Vector3 moveDirection, Vector3 velocity, float velocityY)
        {
            float traversal =  new Vector2(velocity.x, velocity.z).magnitude;
            animator.SetInteger(Move, Mathf.FloorToInt(traversal));
            animator.SetInteger(Jump, Mathf.FloorToInt(velocity.y));
        }
    }
}