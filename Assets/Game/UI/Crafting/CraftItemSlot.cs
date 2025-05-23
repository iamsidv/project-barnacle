using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class CraftItemSlot : ItemSlot
    {
        [SerializeField] private Image image;

        private Action _slotOccupiedCallback;
        
        public override void ReleaseSlot()
        {
            base.ReleaseSlot();
            image.color = Color.white;
        }

        protected override void OnSlotOccupied()
        {
            image.color = Color.yellow;
            _slotOccupiedCallback?.Invoke();
        }

        public void SetCallback(Action callback)
        {
            _slotOccupiedCallback = callback;
        }
    }
}