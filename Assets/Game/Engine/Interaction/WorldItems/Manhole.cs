using Game.UI.MiniGames.ManholeGame;
using UnityEngine;

namespace Game.Engine.Interaction.WorldItems
{
    public class Manhole : BaseInteractableWorldItem
    {
        private readonly int _openState = Animator.StringToHash("Open");
        private readonly int _closeState = Animator.StringToHash("Close");
        
        [SerializeField] private Animator manholeAnimator;
        [SerializeField] private Camera localCamera;
        [SerializeField] private ManholeMinigameView viewPrefab;
        
        private ManholeMinigameView _gameView;
        
        public override void OnInteract()
        {
            SetupCamera();

            if (_gameView == null)
            {
                _gameView = Instantiate(viewPrefab);
                _gameView.gameObject.name = nameof(ManholeMinigameView);
            }

            manholeAnimator.Play(_openState);
            _gameView.SetVisibility(true);
            _gameView.BindWorldItemToView(this);
            _gameView.OnScreenEnter();
        }
        
        private void SetupCamera()
        {
            localCamera.gameObject.SetActive(true);
        }
        
        public void ExitGameMode()
        {
            if (_gameView)
            {
                _gameView.OnScreenExit();
                _gameView.SetVisibility(false);
                localCamera.gameObject.SetActive(false);
                manholeAnimator.Play(_closeState);
            }
        }

        public void RefreshState()
        {
            manholeAnimator.Play(_openState);
        }
    }
}