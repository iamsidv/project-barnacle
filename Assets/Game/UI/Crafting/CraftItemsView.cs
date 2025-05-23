using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class CraftItemsView : BaseView
    {
        [SerializeField] private CraftItemsSection craftingSection;
        [SerializeField] private InventorySection inventorySection;

        [SerializeField] private Button btnContinue;
        [SerializeField] private Button btnClear;

        public CraftItemsSection CraftingSection => craftingSection;
        
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
        }

        private void CraftNewItems()
        {
        }
    }
}