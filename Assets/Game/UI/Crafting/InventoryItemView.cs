using Game.Configs;
using Game.Engine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class InventoryItemView : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Vector3 _startPosition;
        private InventorySection _owner;
        private string _id;
        [SerializeField] private Image image;
        
        //[SerializeField] private Transform _parentTransform;

        public ItemSlot currentSlot;
        
        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        public void SetOwner(InventorySection inventorySection, ItemSlot slot)
        {
            _owner = inventorySection;
            currentSlot = slot;
        }

        public void SetData(PlayerContext context, string id)
        {
            _id = id;
            gameObject.name = id;
           
            CollectibleItem item  = context.Config.CraftConfig.Collectibles.Find(item => item.Id.Equals(id));
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
            transform.SetParent(_owner.transform.parent);
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

            bool result = _owner.CheckOverlaps(this, eventData.position, out ItemSlot itemSlot);

            if (result)
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