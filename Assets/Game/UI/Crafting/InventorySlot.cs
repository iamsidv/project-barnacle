using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class InventorySlot : ItemSlot
    {
        [SerializeField] private Image bg;
        private string _id;

        public void SetData(string s)
        {
            _id = s;
        }

        public void Dispose()
        {
            Destroy(this.gameObject);
        }

        public void SetSprite(Sprite sprite)
        {
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}