using Game.UI.Crafting;
using UnityEngine;

namespace Game.Engine.Interaction.WorldItems
{
    public class Workbench : BaseInteractableWorldItem
    {
        [SerializeField] private CraftItemsView viewPrefab;
        
        private CraftItemsView _gameView;
        
        public override void OnInteract()
        {
            if (_gameView == null)
            {
                _gameView = Instantiate(viewPrefab);
                _gameView.gameObject.name = nameof(CraftItemsView);
            }

            //manholeAnimator.Play(_openState);
            //_gameView.Setup(miniGameConfig);
            _gameView.SetVisibility(true);
            _gameView.OnScreenEnter();
        }
    }
}
