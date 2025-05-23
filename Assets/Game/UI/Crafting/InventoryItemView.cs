using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class InventoryItemView : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPosition;
        private InventorySection _owner;
        private string id;
        [SerializeField] private Image image;


        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     Debug.Log($"IPointerDown {gameObject.name} {transform.position}");
        // }
        //
        // public void OnBeginDrag(PointerEventData eventData)
        // {
        //     Debug.Log($"OnBeginDrag {gameObject.name} {transform.position}");
        // }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log($"OnDrag {gameObject.name}");

            transform.position = eventData.position;
        }

        // public void OnPointerMove(PointerEventData eventData)
        // {
        //     Debug.Log($"OnPointerMove {gameObject.name}");
        //     //transform.position = eventData.position;
        // }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"OnEndDrag {gameObject.name} {eventData.position} {eventData.pointerCurrentRaycast}");
            transform.localPosition = _startPosition;

            // RectTransformUtility.RectangleContainsScreenPoint()
            bool result = _owner.CheckOverlaps(this, eventData.position, out var itemIndicator);

            if (result)
            {
                itemIndicator.SetData(id, null);
                transform.SetParent(itemIndicator.transform);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.localPosition = _startPosition;
            }

            //var result = eventData.pointerCurrentRaycast;
        }

        public void SetOwner(InventorySection inventorySection)
        {
            _owner = inventorySection;
        }

        public void SetData(string s)
        {
            id = s;
        }
    }
}