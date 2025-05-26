using System.Collections.Generic;
using System.Linq;
using Game.Profile;
using UnityEngine;

namespace Game.Engine.Actions
{
    public class AddItemToInventoryAction : IPlayerAction
    {
        public InventoryItem Item { get; }

        public AddItemToInventoryAction(InventoryItem item)
        {
            Item = item;
        }

        public ActionResult Execute(PlayerContext context)
        {
            if (!context.Config.CraftConfig.Collectibles.Any(t => t.Id.Equals(Item.Id)))
            {
                return ActionResult.InvalidState;
            }

            KeyValuePair<int, Slot> kvp = context.Player.Inventory.Slots.FirstOrDefault(kvp => kvp.Value.IsEmpty);

            Slot slot = kvp.Value;

            if (slot == null)
            {
                return ActionResult.Failure;
            }

            slot.AddItem(Item);

            Debug.Log($"Adding {Item.Id} to Slot {slot.Id}");

            return ActionResult.Success;
        }
    }
}