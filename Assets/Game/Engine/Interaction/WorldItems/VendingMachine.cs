using System;
using Game.Configs;
using Game.UI.MiniGames.VendingMachineGame;
using UnityEngine;

namespace Game.Engine.Interaction.WorldItems
{
    public class VendingMachine : BaseInteractableWorldItem
    {
        private readonly int _idleState = Animator.StringToHash("Idle");
        private readonly int _rotateKnobState = Animator.StringToHash("RotateKnob");
        private readonly int _openLidState = Animator.StringToHash("OpenLid");
        private readonly int _brokenState = Animator.StringToHash("MachineBroke");

        [SerializeField] private Animator machineAnimator;
        [SerializeField] private Camera localCamera;
        [SerializeField] private MiniGameConfig miniGameConfig;
        [SerializeField] private VendingMachineGameView viewPrefab;
        [SerializeField] private AudioSource knobAudioSource;
        
        private VendingMachineGameView _gameView;

        private void Start()
        {
            machineAnimator.Play(IsMachineBroke()?_brokenState : _idleState);
        }

        public override void OnInteract()
        {
            GameManager.Instance.SetHeroVisibility(false);
            SetupCamera();

            if (_gameView == null)
            {
                _gameView = Instantiate(viewPrefab);
                _gameView.gameObject.name = nameof(VendingMachineGameView);
            }

            _gameView.Setup(miniGameConfig);
            _gameView.SetVisibility(true);
            _gameView.BindWorldItemToView(this);
            _gameView.OnScreenEnter();
        }

        private void SetupCamera()
        {
            localCamera.gameObject.SetActive(true);
        }

        public void PlayKnobRotateAnimation()
        {
            knobAudioSource.Stop();
            machineAnimator.Play(_rotateKnobState);
            knobAudioSource.Play();
        }

        public void PlayLidOpenAnimation()
        {
            machineAnimator.Play(_openLidState);
        }

        public void OnKnobRotationComplete()
        {
            if (_gameView.TryProcessRewards())
            {
                _gameView.ActivateCollectButton(true);
            }
            knobAudioSource.Stop();
        }

        public void OnLidOpenComplete()
        {
            _gameView.DisplayRewardToUser();
        }

        public void ExitGameMode()
        {
            if (_gameView)
            {
                _gameView.OnScreenExit();
                _gameView.SetVisibility(false);
                localCamera.gameObject.SetActive(false);
            }
            
            GameManager.Instance.SetHeroVisibility(true);
        }

        public void RefreshState()
        {
            machineAnimator.Play(_idleState);
        }

        public void PlayBrokenState()
        {
            machineAnimator.Play(_brokenState);
        }
        
        private bool IsMachineBroke()
        {
            PlayerContext context = GameEngine.Context;
            return context.Player.GetMinigamesPlayed(miniGameConfig) >=
                   miniGameConfig.MaximumPlayCount;
        }
    }
}