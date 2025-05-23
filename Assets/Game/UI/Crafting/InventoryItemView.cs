using Game.Configs;
using Game.Engine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class InventoryItemView : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [SerializeField] private Image image;
        
        private Vector3 _startPosition;
        private InventorySection _inventorySection;
        public string ItemId { get; private set; }
        public int UserSlotId { get; private set; }
        public ItemSlot currentSlot;
        
        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        public void SetOwner(InventorySection inventorySection, ItemSlot slot)
        {
            _inventorySection = inventorySection;
            currentSlot = slot;
        }

        public void SetData(PlayerContext context, string itemId, int slotId)
        {
            ItemId = itemId;
            gameObject.name = itemId;
            UserSlotId = slotId;
           
            CollectibleItem item  = context.Config.CraftConfig.Collectibles.Find(item => item.Id.Equals(itemId));
            if (item != null)
            {
                image.sprite = item.Icon;
            }
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // _parentTransform = transform.parent;
            transform.SetParent(_inventorySection.transform.parent);
            transform.SetAsLastSibling();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            // Debug.Log($"OnDrag {gameObject.name}");
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Debug.Log($"OnEndDrag {gameObject.name} {eventData.position} {eventData.pointerCurrentRaycast}");
            transform.localPosition = _startPosition;

            bool success = _inventorySection.CheckOverlaps(this, eventData.position, out ItemSlot itemSlot);

            if (success)
            {
                currentSlot.ReleaseSlot();
                currentSlot = itemSlot;
                itemSlot.OccupySlot(this);
                transform.SetParent(currentSlot.transform);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.SetParent(currentSlot.transform);
                transform.localPosition = _startPosition;
            }
        }
    }
}