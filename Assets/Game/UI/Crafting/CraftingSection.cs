using System.Collections.Generic;
using System.Linq;
using Game.Configs;
using Game.Engine;
using Game.Profile;
using UnityEngine;

namespace Game.UI.Crafting
{
    public class CraftingSection : MonoBehaviour
    {
        [SerializeField] private CraftItemSlot[] itemSlots;
        [SerializeField] private CraftItemResult result;

        private CraftingRuleSet _itemToCraft;
        private CraftItemsView _craftItemsView;

        public bool CheckOverlap(Vector2 endPosition, out ItemSlot slot)
        {
            foreach (CraftItemSlot itemSlot in itemSlots)
            {
                bool overlaps = RectTransformUtility.RectangleContainsScreenPoint(itemSlot.RectTransform, endPosition);
                if (overlaps && !itemSlot.HasElement())
                {
                    slot = itemSlot;
                    return true;
                }
            }

            slot = null;
            return false;
        }

        public void Init(CraftItemsView owner)
        {
            _craftItemsView = owner;
            foreach (CraftItemSlot itemSlot in itemSlots)
            {
                itemSlot.SetCallback(OnItemSlotOccupied);
            }
        }

        private void OnItemSlotOccupied()
        {
            if (itemSlots.Any(slot => slot.HasElement()))
            {
                List<string> items = new();
                foreach (CraftItemSlot slot in itemSlots)
                {
                    if (slot.HasElement())
                    {
                        items.Add(slot.ItemId);
                    }
                }

                CraftingRuleSet craftingRuleSet = GetCraftRulesetFromMaterials(items);

                if (craftingRuleSet != null)
                {
                    result.PromptSuccess(craftingRuleSet.Icon, craftingRuleSet.ItemName);
                    _itemToCraft = craftingRuleSet;
                    return;
                }

                _itemToCraft = null;
                result.PromptFailure();
            }
            else
            {
                _itemToCraft = null;
                result.Refresh();
            }
        }

        public void Refresh()
        {
            ResetSlots();
        }

        public void ResetSlots()
        {
            result.Refresh();
            foreach (CraftItemSlot itemSlot in itemSlots)
            {
                itemSlot.ResetSlot();
            }
        }

        public void CraftItem()
        {
            if (_itemToCraft != null)
            {
                foreach (CraftItemSlot itemSlot in itemSlots)
                {
                    if (!itemSlot.HasElement())
                    {
                        continue;
                    }
                    
                    if (GameEngine.Context.Player.Inventory.Slots.TryGetValue(itemSlot.UserSlotId, out Slot slot))
                    {
                        slot.RemoveItem();
                    }
                }

                GameEngine.Context.Player.CraftItem(_itemToCraft.ItemName, out int itemsLength);
                
                ResetSlots();

                _craftItemsView.OnCraftingComplete(_itemToCraft, itemsLength - 1);
            }
        }

        private CraftingRuleSet GetCraftRulesetFromMaterials(IReadOnlyList<string> materials)
        {
            List<CraftingRuleSet> craftRules = GameEngine.Context.Config.CraftConfig.CraftingRules;

            foreach (CraftingRuleSet craftRule in craftRules)
            {
                int count = craftRule.Collectables.Count;
                int totalMaterials = materials.Count;

                if (totalMaterials != count)
                {
                    continue;
                }

                int matchingMaterials = 0;
                foreach (var material in materials)
                {
                    if (craftRule.HasCollectable(material))
                    {
                        matchingMaterials += 1;
                    }
                }

                if (count == matchingMaterials)
                {
                    return craftRule;
                }
            }

            return null;
        }
    }
}