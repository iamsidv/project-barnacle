using UnityEngine;

namespace Game.Engine.Interaction
{
    public class InteractableView : MonoBehaviour
    {
        private IPlayerInteractable _interactable;

        private bool _isInteractable;

        private void Awake()
        {
            _isInteractable = false;
        }

        public void Bind(IPlayerInteractable interactable)
        {
            _interactable = interactable;
        }

        public void ShowHint()
        {
            _isInteractable = enabled = true;
        }

        public void HideHint()
        {
            _isInteractable = enabled = false;
        }

        public void ToggleMessage()
        {
            _isInteractable = !_isInteractable;
        }

        private void Update()
        {
            if (!_isInteractable)
            {
                enabled = false;
                return;
            }

            if (Input.GetButtonDown("Interact"))
            {
                _interactable?.OnInteract();
                _isInteractable = false;
            }
        }
    }
}