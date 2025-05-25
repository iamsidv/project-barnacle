using System;
using UnityEngine;

namespace Game.UI.Hud
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offsetPosition;

        public static HudView Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            SetVisibility(false);
        }

        public void AttachToTarget(Transform other, Vector3 offset)
        {
            target = other;
            offsetPosition = offset;
            SetVisibility(true);
        }

        public void ReleaseTarget()
        {
            target = null;
            SetVisibility(false);
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }


            if (Camera.main != null)
            {
                rectTransform.position = Camera.main.WorldToScreenPoint(target.position + offsetPosition);
            }
        }

        private void SetVisibility(bool state)
        {
            rectTransform.gameObject.SetActive(state);
        }
    }
}