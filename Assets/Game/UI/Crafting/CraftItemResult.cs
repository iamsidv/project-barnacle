using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class CraftItemResult : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameField;
        [SerializeField] private Image icon;
        [SerializeField] private Image outline;
        [SerializeField] private GameObject[] mysteryObjects;

        public void Refresh()
        {
            foreach (GameObject go in mysteryObjects)
            {
                go.SetActive(true);
            }
            nameField.text = String.Empty;
            icon.sprite = null;
            outline.color = Color.white;
            icon.gameObject.SetActive(false);
        }
        
        public void PromptSuccess(Sprite sprite, string itemName)
        {
            icon.sprite = sprite;
            nameField.text = itemName;
            outline.color = Color.green;
            icon.gameObject.SetActive(true);
            
            foreach (GameObject go in mysteryObjects)
            {
                go.SetActive(false);
            }
        }
        
        public void PromptFailure()
        {
            Refresh();
            outline.color = Color.red;
        }
    }
}