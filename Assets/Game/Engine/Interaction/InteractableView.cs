using UnityEngine;

namespace Game.Engine.Interaction
{
    public class InteractableView : MonoBehaviour
    {
        private IPlayerInteractable _interactingObject;

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
            var direction = Camera.main.transform.position - transform.position;
            direction.y = 0;
            view.transform.rotation = Quaternion.LookRotation(-direction.normalized);

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
                if (view != null)
                {
                    view.SetActive(_isActive);
                }
            }
        }
    }
}