using Game.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.UI.MiniGames
{
    public class MinigameRewardGenerator
    {
        private readonly MiniGameConfig _config;
        
        public MinigameRewardGenerator(MiniGameConfig config)
        {
            _config = config;
        }

        public string GetReward()
        {
            int value = Mathf.FloorToInt(Random.value * 100);
            int lowerLimit = 0;
            foreach (MiniGameRewardData data in _config.possibleRewards)
            {
                if (value > lowerLimit && value <= data.probability + lowerLimit)
                {
                    return data.ItemId;
                }

                lowerLimit = data.probability;
            }
            return string.Empty;
        }
    }
}