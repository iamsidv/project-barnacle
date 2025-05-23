using System.Linq;
using Game.Profile;

namespace Game.Engine.Actions
{
    public class AddInventoryAction : IPlayerAction
    {
        public int SlotId { get; }
        public InventoryItem Item { get; }
        
        public AddInventoryAction(int slotId, InventoryItem item)
        {
            SlotId = slotId;
            Item = item;
        }

        public ActionResult Execute(PlayerContext context)
        {
            if (!context.Config.CraftConfig.Collectibles.Any(t => t.Id.Equals(Item.Id)))
            {
                return ActionResult.InvalidState;
            }

            if (!context.Player.Inventory.Slots.TryGetValue(SlotId, out Slot slot))
            {
                return ActionResult.InvalidState;
            }

            if (!slot.IsEmpty)
            {
                return ActionResult.Failure;
            }

            slot.AddItem(Item);
            
            return ActionResult.Success;
        }
    }
}