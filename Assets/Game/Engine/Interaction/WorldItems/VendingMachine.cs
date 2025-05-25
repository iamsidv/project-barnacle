using Game.UI.MiniGames.VendingMachineGame;
using UnityEngine;

namespace Game.Engine.Interaction.WorldItems
{
    public class VendingMachine : BaseInteractableWorldItem
    {
        private readonly int IdleState = Animator.StringToHash("Idle");
        private readonly int RotateKnobState = Animator.StringToHash("RotateKnob");
        private readonly int OpenLidState = Animator.StringToHash("OpenLid");
        
        [SerializeField] private Animator machineAnimator;
        [SerializeField] private Camera localCamera;

        [SerializeField] private VendingMachineGameView gameView;
        
        public override void OnInteract()
        {
            SetupCamera();
            gameView.BindWorldItemToView(this);
            gameView.OnScreenEnter();
        }

        private void SetupCamera()
        {
            localCamera.gameObject.SetActive(true);
        }

        public void OnKnobRotationComplete()
        {
            
        }
        
        public void OnLidOpenComplete()
        {
            
        }
    }
}