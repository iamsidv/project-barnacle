using Game.UI.Hud;
using UnityEngine;

namespace Game.Engine.Interaction
{
    public class InteractableView : MonoBehaviour
    {
        private IPlayerInteractable _interactingObject;

        [SerializeField] private Vector3 offsetPosition;
        [SerializeField] private GameObject view;

        private bool _isActive;

        private void Awake()
        {
            Interactable = false;
        }

        public void Bind(IPlayerInteractable interactable)
        {
            _interactingObject = interactable;
        }

        public void ShowHint()
        {
            Interactable = true;
        }

        public void HideHint()
        {
            Interactable = false;
        }

        public void ToggleMessage()
        {
            Interactable = !Interactable;
        }

        private void Update()
        {
            if (!Interactable)
            {
                return;
            }

            if (Input.GetButtonDown("Interact"))
            {
                _interactingObject?.OnInteract();
                Interactable = false;
            }
        }

        private bool Interactable
        {
            get => _isActive;
            set
            {
                _isActive = enabled = value;

                if (_isActive)
                {
                    HudView.Instance.AttachToTarget(transform, offsetPosition);
                }
                else
                {
                    HudView.Instance.ReleaseTarget();
                }
            }
        }
    }
}