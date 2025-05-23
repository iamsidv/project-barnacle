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

            Debug.Log($"_debug_ a : {a}, b : {b}");
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
        }

        public void Refresh()
        {
        }

        public void ResetSlot()
        {
            item1.ResetSlot();
            item2.ResetSlot();
        }
    }
}