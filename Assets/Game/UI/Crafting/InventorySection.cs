using System.Collections.Generic;
using Game.Engine;
using Game.Profile;
using UnityEngine;

namespace Game.UI.Crafting
{
    public class InventorySection : MonoBehaviour
    {
        [SerializeField] private InventorySlotView slotPrefab;
        [SerializeField] private InventoryItemView inventoryItemPrefab;
        [SerializeField] private Transform inventoryContainer;

        private CraftItemsView _owner;
        private readonly List<InventorySlotView> _slotItems = new();

        public void Init(CraftItemsView owner)
        {
            _owner = owner;
        }

        public void Refresh()
        {
            Clear();
            PopulateInventory();
        }

        private void PopulateInventory()
        {
            PlayerContext context = GameEngine.Context;

            foreach ((int slotId, Slot slot) in context.Player.Inventory.Slots)
            {
                InventorySlotView view = Instantiate(slotPrefab, inventoryContainer);
                view.SetData(slotId.ToString());
                view.SetVisibility(true);
                _slotItems.Add(view);

                if (slot.Item != null)
                {
                    InventoryItemView item = Instantiate(inventoryItemPrefab, view.transform);
                    item.SetData(context, slot.Item.Id);
                    item.SetOwner(this);
                    item.SetVisibility(true);
                }
            }
        }

        private void Clear()
        {
            foreach (InventorySlotView item in _slotItems)
            {
                item.Dispose();
            }

            _slotItems.Clear();
        }

        public bool CheckOverlaps(InventoryItemView view, Vector2 eventDataPosition, out CraftItemIndicator indicator)
        {
            return _owner.CraftingSection.CheckOverlap(eventDataPosition, out indicator);
        }
    }
}