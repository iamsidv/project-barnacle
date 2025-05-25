using System;
using Game.Configs;
using Game.Engine;
using Game.Engine.Actions;
using Game.Engine.Interaction;
using Game.Engine.Interaction.WorldItems;
using Game.Profile;
using Game.UI.Minigames;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MiniGames.VendingMachineGame
{
    public class VendingMachineGameView : BaseView, IMinigame
    {
        private readonly float _timeToBlockTintInput = 2f;

        [SerializeField] private Button btnExit;
        [SerializeField] private Button btnCollectItem;
        [SerializeField] private Button btnRotateKnob;
        [SerializeField] private VendingMachine vendingMachine;

        [SerializeField] private Image rewardImage;
        [SerializeField] private TMP_Text rewardName;
        [SerializeField] private Animation rewardContainer;
        [SerializeField] private MiniGameConfig miniGameConfig;

        [SerializeField] private Button tint;

        private MinigameRewardGenerator _rewardGenerator;
        private float _tintDisplayStartTime;

        public void Setup(MiniGameConfig config)
        {
            miniGameConfig = config;
        }

        public void BindWorldItemToView(BaseInteractableWorldItem machine)
        {
            if (vendingMachine == null)
            {
                vendingMachine = (VendingMachine)machine;
                _rewardGenerator = new MinigameRewardGenerator(miniGameConfig);
            }
        }

        public override void OnScreenEnter()
        {
            btnExit.onClick.AddListener(OnExit);
            btnCollectItem.onClick.AddListener(OnCollectItem);
            btnRotateKnob.onClick.AddListener(OnRotateKnob);
            tint.onClick.AddListener(OnRewardSkipClicked);

            btnExit.interactable = true;
            btnCollectItem.gameObject.SetActive(false);
            btnRotateKnob.interactable = CanRotateKnob();
        }

        public override void OnScreenExit()
        {
            btnExit.onClick.RemoveAllListeners();
            btnCollectItem.onClick.RemoveAllListeners();
            btnRotateKnob.onClick.RemoveAllListeners();

            btnExit.interactable = false;
            btnCollectItem.gameObject.SetActive(false);
            btnRotateKnob.interactable = false;
        }

        private void OnRotateKnob()
        {
            if (!CanRotateKnob())
            {
                vendingMachine.PlayBrokenState();
                return;
            }

            vendingMachine.PlayKnobRotateAnimation();
            _rewardGenerator.GetReward();
            btnRotateKnob.gameObject.SetActive(false);
        }

        private void OnCollectItem()
        {
            vendingMachine.PlayLidOpenAnimation();
            ActivateCollectButton(false);
        }

        private void OnExit()
        {
            vendingMachine.ExitGameMode();
            rewardContainer.gameObject.SetActive(false);
        }

        private bool CanRotateKnob()
        {
            PlayerContext context = GameEngine.Context;
            return context.Player.GetMinigamesPlayed(miniGameConfig) <
                   miniGameConfig.MaximumPlayCount;
        }

        public bool TryProcessRewards()
        {
            PlayerContext context = GameEngine.Context;
            context.Player.UpdateMinigamePlayedCount(miniGameConfig.MinigameId);
            return true;
        }

        public void ActivateCollectButton(bool isActive)
        {
            btnCollectItem.gameObject.SetActive(isActive);
        }

        public void DisplayRewardToUser()
        {
            string rewardId = _rewardGenerator.GetReward();
            PlayerContext context = GameEngine.Context;
            CollectibleItem rewardItem = context.Config.CraftConfig.Collectibles.Find(item => item.Id.Equals(rewardId));
            tint.gameObject.SetActive(true);
            if (rewardItem != null)
            {
                rewardImage.sprite = rewardItem.Icon;
                rewardName.text = rewardItem.Id;
                rewardContainer.gameObject.SetActive(true);
                rewardContainer.Play();
                IPlayerAction playerAction = new AddItemToInventoryAction(new InventoryItem(rewardItem.Id));

                ActionResult result = playerAction.Execute(GameEngine.Context);
                if (result != ActionResult.Success)
                {
                    Debug.Log("Inventory Already Full");
                }
                else
                {
                }
            }

            btnRotateKnob.gameObject.SetActive(true);
        }

        [UsedImplicitly]
        public void OnRewardSkipClicked()
        {
            if (Time.time - _timeToBlockTintInput >= _tintDisplayStartTime)
            {
                tint.gameObject.SetActive(false);
                vendingMachine.RefreshState();
            }
        }
    }
}