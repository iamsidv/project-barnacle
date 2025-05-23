using UnityEngine;

namespace Game.UI.Crafting
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private int slotId;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] protected bool occupied;
        [SerializeField] private InventoryItemView inventoryItem;
        
        public RectTransform RectTransform => rectTransform;
        
        public bool HasElement()
        {
            return occupied;
        }

        public void OccupySlot(InventoryItemView item)
        {
            inventoryItem = item;
            occupied = true;
        }

        public void ReleaseSlot()
        {
            inventoryItem = null;
            occupied = false;
        }
    }
}