using System.Collections.Generic;
using Game.Profile;

namespace Game.UI.Minigames
{
    public class MinigameFactory : BaseSingleton<MinigameFactory>
    {
        public List<BaseMiniGame> MiniGames;
        
    }
}