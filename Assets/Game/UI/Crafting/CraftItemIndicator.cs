using UnityEngine;

namespace Game.UI.Crafting
{
    public class CraftItemIndicator : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        public RectTransform RectTransform => rectTransform;

        private bool _isOccupied;
        
        public void SetData(string id, Sprite sprite)
        {
            _isOccupied = true;
        }

        public bool HasElement()
        {
            return _isOccupied;
        }
    }
}