using System.Collections.Generic;
using Game.Configs;
using Game.Engine;
using Game.Profile;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.UI.Crafting
{
    public class CraftingSection : MonoBehaviour
    {
        [SerializeField] private CraftItemSlot itemSlot1;
        [SerializeField] private CraftItemSlot itemSlot2;
        [SerializeField] private CraftItemResult result;

        private CraftItemsView _owner;

        public bool CheckOverlap(Vector2 endPosition, out ItemSlot slot)
        {
            bool a = RectTransformUtility.RectangleContainsScreenPoint(itemSlot1.RectTransform, endPosition);
            bool b = RectTransformUtility.RectangleContainsScreenPoint(itemSlot2.RectTransform, endPosition);

            //Debug.Log($"_debug_ a : {a}, b : {b}");
            if (a && !itemSlot1.HasElement())
            {
                slot = itemSlot1;
                return true;
            }

            if (b && !itemSlot2.HasElement())
            {
                slot = itemSlot2;
                return true;
            }

            slot = null;
            return false;
        }

        public void Init(CraftItemsView owner)
        {
            _owner = owner;
            itemSlot1.SetCallback(OnItemSlotOccupied);
            itemSlot2.SetCallback(OnItemSlotOccupied);
        }

        private void OnItemSlotOccupied()
        {
            if (itemSlot1.HasElement() && itemSlot2.HasElement())
            {
                CraftingRuleSet craftingRuleSet = GetCraftRulesetFromMaterials(itemSlot1.ItemId, itemSlot2.ItemId);

                if (craftingRuleSet != null)
                {
                    result.PromptSuccess(craftingRuleSet.Icon, craftingRuleSet.ItemName);
                    return;
                }

                result.PromptFailure();
            }
            else
            {
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
            itemSlot1.ResetSlot();
            itemSlot2.ResetSlot();
        }

        public void CraftItem()
        {
            if (itemSlot1.HasElement() && itemSlot2.HasElement())
            {
                CraftingRuleSet craftingRuleSet = GetCraftRulesetFromMaterials(itemSlot1.ItemId, itemSlot2.ItemId);
                if (craftingRuleSet != null)
                {
                    if (_owner.InventorySection.Slots.TryGetValue(itemSlot1.UserSlotId, out Slot slot))
                    {
                        slot.RemoveItem();
                    }

                    if (_owner.InventorySection.Slots.TryGetValue(itemSlot2.UserSlotId, out Slot slot2))
                    {
                        slot2.RemoveItem();
                    }

                    if (GameEngine.Context.Player.Inventory.Slots.TryGetValue(itemSlot1.UserSlotId, out Slot s1))
                    {
                        s1.RemoveItem();
                    }

                    if (GameEngine.Context.Player.Inventory.Slots.TryGetValue(itemSlot1.UserSlotId, out Slot s2))
                    {
                        s2.RemoveItem();
                    }

                    ResetSlots();
                }
            }
        }

        private CraftingRuleSet GetCraftRulesetFromMaterials(string material1, string material2)
        {
            List<CraftingRuleSet> craftRules = GameEngine.Context.Config.CraftConfig.CraftingRules;

            foreach (CraftingRuleSet craftRule in craftRules)
            {
                int count = craftRule.Collectables.Count;
                if (craftRule.HasCollectable(material1) &&
                    craftRule.HasCollectable(material2) && count == 2)
                {
                    return craftRule;
                }
            }

            return null;
        }
    }
}