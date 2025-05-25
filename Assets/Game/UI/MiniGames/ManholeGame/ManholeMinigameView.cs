using System.Collections;
using System.Collections.Generic;
using Game.Configs;
using Game.Engine;
using Game.Engine.Actions;
using Game.Engine.Interaction;
using Game.Engine.Interaction.WorldItems;
using Game.Profile;
using Game.UI.Minigames;
using Game.UI.Minigames.Manhole;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.UI.MiniGames.ManholeGame
{
    public class ManholeMinigameView : BaseView, IMinigame
    {
        private readonly float _timeToBlockTintInput = 2f;

        public TMP_Text questionText;
        public Button[] optionButtons;
        public TMP_Text npcText;

        [SerializeField] private TextAsset questionsTextAsset;

        [Header("Intro Settings")] [TextArea]
        public string npcIntroText = "Hello there! Let's see how smart you are. Ready for some questions?";

        public float typingSpeed = 0.05f;

        [Header("NPC Responses")] public string correctResponse;

        public string[] wrongResponses;

        [SerializeField] private List<QuestionData> questions;
        [SerializeField] private int currentQuestionIndex = 0;
        [SerializeField] private GameObject questionContainer;
        [SerializeField] private GameObject optionsContainer;

        [SerializeField] private Manhole manhole;
        [SerializeField] private Button tint;

        [SerializeField] private Image rewardImage;
        [SerializeField] private TMP_Text rewardName;
        [SerializeField] private Animation rewardContainer;
        [SerializeField] private MiniGameConfig miniGameConfig;

        [SerializeField] private Button btnExit;

        private MinigameRewardGenerator _rewardGenerator;
        private float _tintDisplayStartTime;
        private QuestionListWrapper _wrapper;

        private void Awake()
        {
            LoadQuestions();
        }

        IEnumerator PlayIntroText()
        {
            LoadDynamicTexts();
            questionContainer.SetActive(false);
            optionsContainer.SetActive(false);

            npcText.text = "";
            foreach (char c in npcIntroText)
            {
                npcText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(1f);
            ShowQuestion();
        }

        void LoadQuestions()
        {
            _wrapper = JsonConvert.DeserializeObject<QuestionListWrapper>(questionsTextAsset.text);
            questions = new List<QuestionData>(_wrapper.questions);
        }

        void LoadDynamicTexts()
        {
            npcIntroText = _wrapper.introduction[Random.Range(0, _wrapper.introduction.Count)];
            wrongResponses = _wrapper.wrongResponse.ToArray();
            correctResponse = _wrapper.correctResponse[Random.Range(0, _wrapper.correctResponse.Count)];
        }

        void ShowQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                npcText.text = "You've completed the quiz!";
                ClearView();
                return;
            }

            questionContainer.SetActive(true);
            optionsContainer.SetActive(true);

            QuestionData q = questions[currentQuestionIndex];
            questionText.text = q.question;
            for (int i = 0; i < optionButtons.Length; i++)
            {
                int choiceIndex = i;
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = q.options[i];
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => CheckAnswer(choiceIndex));
            }
        }

        private void ClearView()
        {
            questionText.text = string.Empty;
            foreach (var btn in optionButtons)
            {
                btn.gameObject.SetActive(false);
            }

            questionContainer.SetActive(false);
            optionsContainer.SetActive(false);
        }

        void CheckAnswer(int selectedIndex)
        {
            var q = questions[currentQuestionIndex];
            npcText.text = (selectedIndex == q.correctIndex)
                ? correctResponse
                : wrongResponses[Random.Range(0, wrongResponses.Length)];

            currentQuestionIndex++;
            // Invoke("ShowQuestion", 1.5f);
            
            GameEngine.Context.Player.UpdateMinigamePlayedCount(miniGameConfig.MinigameId);
            
            if (selectedIndex == q.correctIndex)
            {
                ClearView();
                DisplayRewardToUser();
            }
            else
            {
                ClearView();
                //Invoke("ShowQuestion", 1.5f);
            }
        }

        [System.Serializable]
        private class QuestionListWrapper
        {
            public QuestionData[] questions;
            public List<string> introduction;
            public List<string> wrongResponse;
            public List<string> correctResponse;
            public List<string> laterResponse;
        }

        public void BindWorldItemToView(BaseInteractableWorldItem referenceManhole)
        {
            if (manhole == null)
            {
                manhole = (Manhole)referenceManhole;
                _rewardGenerator = new MinigameRewardGenerator(miniGameConfig);
            }
        }
        
        public void Setup(MiniGameConfig config)
        {
            miniGameConfig = config;
        }

        private void OnExit()
        {
            manhole.ExitGameMode();
            rewardContainer.gameObject.SetActive(false);
        }

        [UsedImplicitly]
        public void OnRewardSkipClicked()
        {
            if (Time.time - _timeToBlockTintInput >= _tintDisplayStartTime)
            {
                tint.gameObject.SetActive(false);
                manhole.RefreshState();
            }
        }

        public override void OnScreenEnter()
        {
            btnExit.onClick.AddListener(OnExit);
            tint.onClick.AddListener(OnRewardSkipClicked);
            btnExit.interactable = true;

            if (GameEngine.Context.Player.GetMinigamesPlayed(miniGameConfig) >= miniGameConfig.MaximumPlayCount)
            {
                StartCoroutine(PlayComebackLater());
            }
            else
            {
                StartCoroutine(PlayIntroText());
            }
        }

        private IEnumerator PlayComebackLater()
        {
            questionContainer.SetActive(false);
            optionsContainer.SetActive(false);

            npcIntroText = _wrapper.laterResponse[Random.Range(0, _wrapper.laterResponse.Count)];
            npcText.text = "";
            foreach (char c in npcIntroText)
            {
                npcText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        public override void OnScreenExit()
        {
            btnExit.onClick.RemoveAllListeners();
            btnExit.interactable = false;
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
        }
    }
}