using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class InventoryItemView : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Vector3 _startPosition;
        private InventorySection _owner;
        private string id;
        [SerializeField] private Image image;
        
        [SerializeField] private Transform _parentTransform;

        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        public void SetOwner(InventorySection inventorySection)
        {
            _owner = inventorySection;
        }

        public void SetData(string s)
        {
            id = s;
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _parentTransform = transform.parent;
            transform.SetParent(_owner.transform.parent);
            transform.SetAsLastSibling();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log($"OnDrag {gameObject.name}");
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"OnEndDrag {gameObject.name} {eventData.position} {eventData.pointerCurrentRaycast}");
            transform.localPosition = _startPosition;

            bool result = _owner.CheckOverlaps(this, eventData.position, out var itemIndicator);

            if (result)
            {
                itemIndicator.SetData(id, null);
                transform.SetParent(itemIndicator.transform);
                transform.localPosition = Vector3.zero;
                _parentTransform = itemIndicator.transform;
            }
            else
            {
                transform.SetParent(_parentTransform);
                transform.localPosition = _startPosition;
            }
        }
    }
}