using UnityEngine;

namespace Game.Engine.Movement
{
    public class HeroAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        public void LogData(Vector3 moveDirection, Vector3 velocity, float velocityY)
        {
            // Debug.Log($"LogData : Dir: {moveDirection}, Velocity: {velocity}, yVelocity: {velocityY}");

            float tt =  new Vector2(velocity.x, velocity.z).magnitude;
            
            // Debug.Log(Mathf.FloorToInt(tt));
            
            animator.SetInteger("Move", Mathf.FloorToInt(tt));
            animator.SetInteger("Jump", Mathf.FloorToInt(velocity.y));
            // animator.SetFloat("vY", velocity.y);
            // animator.SetFloat("vZ", velocity.z);
        }
    }
}