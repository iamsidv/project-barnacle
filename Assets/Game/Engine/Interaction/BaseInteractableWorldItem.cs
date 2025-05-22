using UnityEngine;

namespace Game.Engine.Interaction
{
    [RequireComponent(typeof(InteractableView))]
    public abstract class BaseInteractableWorldItem : MonoBehaviour, IPlayerInteractable
    {
        private InteractableView _interactableView;

        private void Awake()
        {
            if (_interactableView == null)
            {
                BindToView();
            }
        }

        public abstract void OnInteract();

        private void OnValidate()
        {
            BindToView();
        }

        private void BindToView()
        {
            _interactableView = GetComponent<InteractableView>();
            _interactableView?.Bind(this);
        }
    }
}