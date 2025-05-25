using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "MiniGameConfig.asset", menuName = "Configs/Minigame", order = -1)]
    public class MiniGameConfig : ScriptableObject
    {
        [SerializeField] public List<MiniGameRewardData> possibleRewards;
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