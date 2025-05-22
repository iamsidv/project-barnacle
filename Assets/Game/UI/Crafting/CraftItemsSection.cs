using UnityEngine;

namespace Game.UI.Crafting
{
    public class CraftItemsSection : MonoBehaviour
    {
        [SerializeField] private CraftItemIndicator item1;
        [SerializeField] private CraftItemIndicator item2;
        [SerializeField] private CraftItemResult result;

        private CraftItemsView _owner;

        public bool CheckOverlap(Vector2 endPosition, out CraftItemIndicator indicator)
        {
            bool a = RectTransformUtility.RectangleContainsScreenPoint(item1.RectTransform, endPosition);
            bool b = RectTransformUtility.RectangleContainsScreenPoint(item2.RectTransform, endPosition);

            Debug.Log($"_debug_ a : {a}, b : {b}");
            if (a && !item1.HasElement())
            {
                indicator = item1;
                return true;
            }

            if (b && !item2.HasElement())
            {
                indicator = item2;
                return true;
            }

            indicator = null;
            return false;
        }

        public void Init(CraftItemsView owner)
        {
            _owner = owner;
        }

        public void Refresh()
        {
        }
    }
}