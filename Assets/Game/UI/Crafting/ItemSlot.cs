using UnityEngine;

namespace Game.UI.Crafting
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private int slotId;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] protected bool occupied;
        [SerializeField] protected InventoryItemView inventoryItem;
        
        public RectTransform RectTransform => rectTransform;
        public int SlotId => slotId;
        public string InventoryItemId => inventoryItem.InventoryItemId;
        
        public void SetData(int id)
        {
            slotId = id;
        }
        
        public bool HasElement()
        {
            return occupied;
        }

        public void OccupySlot(InventoryItemView item)
        {
            inventoryItem = item;
            occupied = true;
            OnSlotOccupied();
        }

        public virtual void ReleaseSlot()
        {
            inventoryItem = null;
            occupied = false;
        }

        protected virtual void OnSlotOccupied()
        {
        }
        
        public void ResetSlot()
        {
            if (inventoryItem != null)
            {
                Destroy(inventoryItem.gameObject);
            }
            
            ReleaseSlot();
        }
    }
}