using System;
using System.Collections.Generic;
using Game.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.UI.Hud
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offsetPosition;

        [SerializeField] private TMP_Text textPrefab;
        [SerializeField] private Transform textContainer;
        private int maxCount = 5;
        [SerializeField] private List<GameObject> volatileGameObjects = new();
        
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

        public void DisplayText(string message)
        {
            if (volatileGameObjects.Count > maxCount)
            {
                var text = volatileGameObjects[0];
                volatileGameObjects.RemoveAt(0);
                Destroy(text.gameObject);
            }
            else
            {
                TMP_Text obj = Instantiate(textPrefab, textContainer);
                obj.gameObject.SetActive(true);
                obj.text = message;
                obj.TryGetComponent(out AutoDestroy autoDestroy);
                autoDestroy.SetOnDestroy(OnObjectDestroyed);
                volatileGameObjects.Add(obj.gameObject);
            }
        }

        private void OnObjectDestroyed(GameObject obj)
        {
            int index = volatileGameObjects.FindIndex(t => t.Equals(obj));
            volatileGameObjects.RemoveAt(index);
        }
    }
}