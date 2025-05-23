using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class CraftItemsView : BaseView
    {
        [SerializeField] private CraftingSection craftingSection;
        [SerializeField] private InventorySection inventorySection;

        [SerializeField] private Button btnContinue;
        [SerializeField] private Button btnClear;

        public CraftingSection CraftingSection => craftingSection;
        
        private void Start()
        {
            OnScreenEnter();
        }

        public override void OnScreenEnter()
        {
            btnContinue.onClick.AddListener(CraftNewItems);
            btnClear.onClick.AddListener(ClearCraftingOptions);
            
            craftingSection.Init(this);
            inventorySection.Init(this);

            craftingSection.Refresh();
            inventorySection.Refresh();
        }

        private void ClearCraftingOptions()
        {
            craftingSection.ResetSlots();
            inventorySection.ResetSlot();
        }

        private void CraftNewItems()
        {
        }
    }
}