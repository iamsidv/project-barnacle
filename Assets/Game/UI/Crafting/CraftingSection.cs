using System.Collections.Generic;
using Game.Configs;
using Game.Engine;
using UnityEngine;

namespace Game.UI.Crafting
{
    public class CraftingSection : MonoBehaviour
    {
        [SerializeField] private CraftItemSlot item1;
        [SerializeField] private CraftItemSlot item2;
        [SerializeField] private CraftItemResult result;

        private CraftItemsView _owner;

        public bool CheckOverlap(Vector2 endPosition, out ItemSlot slot)
        {
            bool a = RectTransformUtility.RectangleContainsScreenPoint(item1.RectTransform, endPosition);
            bool b = RectTransformUtility.RectangleContainsScreenPoint(item2.RectTransform, endPosition);

            //Debug.Log($"_debug_ a : {a}, b : {b}");
            if (a && !item1.HasElement())
            {
                slot = item1;
                return true;
            }

            if (b && !item2.HasElement())
            {
                slot = item2;
                return true;
            }

            slot = null;
            return false;
        }

        public void Init(CraftItemsView owner)
        {
            _owner = owner;
            item1.SetCallback(OnItemSlotOccupied);
            item2.SetCallback(OnItemSlotOccupied);
        }

        private void OnItemSlotOccupied()
        {
            if (item1.HasElement() && item2.HasElement())
            {
                List<CraftingRuleSet> craftRules = GameEngine.Context.Config.CraftConfig.CraftingRules;

                CraftingRuleSet craftingRuleSet = null;
                foreach (CraftingRuleSet craftRule in craftRules)
                {
                    int count = craftRule.Collectables.Count;
                    if (craftRule.HasCollectable(item1.InventoryItemId) &&
                        craftRule.HasCollectable(item2.InventoryItemId) && count == 2)
                    {
                        craftingRuleSet = craftRule;
                        break;
                    }
                }
                
                if (craftingRuleSet!=null)
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
            item1.ResetSlot();
            item2.ResetSlot();
        }
    }
}