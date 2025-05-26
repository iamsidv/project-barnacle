using Game.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Audio
{
    [RequireComponent(typeof(Button))]
    public class UIButtonClickSound : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private void OnValidate()
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }
        }

        private void Awake()
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }
        }

        private void OnEnable()
        {
            button.onClick.AddListener(PlayClickSound);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(PlayClickSound);
        }

        private void PlayClickSound()
        {
            AudioManager.Instance.PlayButtonClickSfx();
        }
    }
}
