using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class CraftItemSlot : ItemSlot
    {
        [SerializeField] private Image image;

        public override void ReleaseSlot()
        {
            base.ReleaseSlot();
            image.color = Color.white;
        }

        protected override void OnSlotOccupied()
        {
            image.color = Color.yellow;
        }
    }
}