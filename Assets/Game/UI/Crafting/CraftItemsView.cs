using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Crafting
{
    public class CraftItemsView : BaseView
    {
        [SerializeField] private CraftItemsSection craftingSection;
        [SerializeField] private InventorySection inventorySection;

        internal readonly List<string> PlayerInventory = new()
        {
            "Cat", "Dog", "Button"
        };

        public CraftItemsSection CraftingSection => craftingSection;
        
        private void Start()
        {
            OnScreenEnter();
        }

        public override void OnScreenEnter()
        {
            craftingSection.Init(this);
            inventorySection.Init(this);

            craftingSection.Refresh();
            inventorySection.Refresh();
        }
    }
}