using Game.Configs;
using Game.Profile;

namespace Game.Engine
{
    public class PlayerContext
    {
        public GameConfig Config { get; private set; }
        public PlayerProfile Player { get; private set; }

        public PlayerContext(GameConfig gameConfig)
        {
            Config = gameConfig;
            Player = new PlayerProfile();
        }
    }
}