using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Game.UI.Crafting
{
    public class InventorySection : MonoBehaviour
    {
        private readonly int _inventorySlots = 6;

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
            for (int i = 0; i < _inventorySlots; i++)
            {
                InventorySlotView view = Instantiate(slotPrefab, inventoryContainer);
                view.SetVisibility(true);
                _slotItems.Add(view);
            }

            for (int i = 0; i < _owner.PlayerInventory.Count; i++)
            {
                InventoryItemView item = Instantiate(inventoryItemPrefab, _slotItems[i].transform);
                item.SetData(_owner.PlayerInventory[i]);
                item.SetOwner(this);
                item.SetVisibility(true);
                item.gameObject.name = _owner.PlayerInventory[i];
                // tt.transform.position = _slotItems[i].transform.position;
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
            //RectTransformUtility.RectangleContainsScreenPoint(_owner.CraftingSection.)
            
            return _owner.CraftingSection.CheckOverlap(eventDataPosition, out indicator);
        }
    }
}