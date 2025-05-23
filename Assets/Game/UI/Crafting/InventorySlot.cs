using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class InventorySlot : ItemSlot
    {
        [SerializeField] private Image bg;
        private string _id;

        public void Dispose()
        {
            Destroy(this.gameObject);
        }

        public void SetSlot(InventoryItemView item)
        {
            inventoryItem = item;
            occupied = true;
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}