using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "MiniGameConfig.asset", menuName = "Configs/Minigame", order = -1)]
    public class MiniGameConfig : ScriptableObject
    {
        [SerializeField] private string minigameId;
        [SerializeField] public List<MiniGameRewardData> possibleRewards;
        [SerializeField] private int maximumTries;

        public string MinigameId => minigameId;
        public int MaximumPlayCount => maximumTries;
    }

    [System.Serializable]
    public class MiniGameRewardData
    {
        public string itemId;
        public int probability;

        public string ItemId => itemId;
        public int Probability => probability;
    }
}