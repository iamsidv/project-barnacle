using Game.Engine.Interaction.WorldItems;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MiniGames.VendingMachineGame
{
    public class VendingMachineGameView : BaseView
    {
        [SerializeField] private Button btnExit;
        [SerializeField] private Button btnCollectItem;
        [SerializeField] private Button btnRotateKnob;
        [SerializeField] private VendingMachine vendingMachine;

        [SerializeField] private Image rewardImage;
        [SerializeField] private RectTransform rewardContainer;
        
        // private VendingMachineController controller = new();
        
        public void BindWorldItemToView(VendingMachine machine)
        {
            vendingMachine = machine;
        }

        public override void OnScreenEnter()
        {
           btnExit.onClick.AddListener(OnExit);
           btnCollectItem.onClick.AddListener(OnCollectItem);
           btnRotateKnob.onClick.AddListener(OnRotateKnob);

           btnExit.interactable = true;
           btnCollectItem.interactable = false;
           btnRotateKnob.interactable = CanRotateKnob();
        }

        private void OnRotateKnob()
        {
        }

        private void OnCollectItem()
        {
        }

        private void OnExit()
        {
        }

        private bool CanRotateKnob()
        {
            return false;
        }
    }
}