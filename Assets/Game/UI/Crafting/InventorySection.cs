using System.Collections.Generic;
using Game.Engine;
using Game.Profile;
using UnityEngine;

namespace Game.UI.Crafting
{
    public class InventorySection : MonoBehaviour
    {
        [SerializeField] private InventorySlot slotPrefab;
        [SerializeField] private InventoryItemView inventoryItemPrefab;
        [SerializeField] private Transform inventoryContainer;

        private CraftItemsView _craftItemsView;
        private readonly List<InventorySlot> _slotItems = new();

        public Dictionary<int, Slot> Slots = new()
        {
            { 1, new Slot(1).AddItem(new InventoryItem("button")) },
            { 2, new Slot(2).AddItem(new InventoryItem("cap")) },
            { 3, new Slot(3).AddItem(new InventoryItem("matchbox")) },
            { 4, new Slot(4) },
            { 5, new Slot(5) },
            { 6, new Slot(6) },
            { 7, new Slot(7) },
            { 8, new Slot(8) }
        };
        
        public void Init(CraftItemsView owner)
        {
            _craftItemsView = owner;
        }

        public void Refresh()
        {
            Clear();
            PopulateInventory();
        }

        private void PopulateInventory()
        {
            PlayerContext context = GameEngine.Context;

            foreach ((int slotId, Slot slot) in /*context.Player.Inventory.*/Slots)
            {
                InventorySlot existingItem = _slotItems.Find(t => t.SlotId == slotId);
                
                InventorySlot view = existingItem == null ? Instantiate(slotPrefab, inventoryContainer) : existingItem;
                view.SetData(slotId);
                view.SetVisibility(true);

                if (existingItem == null)
                {
                    _slotItems.Add(view);
                }

                if (slot.Item != null)
                {
                    InventoryItemView item = Instantiate(inventoryItemPrefab, view.transform);
                    item.SetData(context, slot.Item.Id, slotId);
                    item.SetOwner(this, view);
                    item.SetVisibility(true);
                    view.SetSlot(item);
                }
            }
        }

        private void Clear()
        {
            foreach (InventorySlot item in _slotItems)
            {
                item.Dispose();
            }

            _slotItems.Clear();
        }

        public bool CheckOverlaps(InventoryItemView view, Vector2 eventDataPosition, out ItemSlot slot)
        {
            return _craftItemsView.CraftingSection.CheckOverlap(eventDataPosition, out slot);
        }

        public void ResetSlot()
        {
            foreach (InventorySlot item in _slotItems)
            {
                item.ResetSlot();
            }
            
            PopulateInventory();
        }
    }
}