using Game.Configs;
using Game.Engine.Interaction;

namespace Game.UI.Minigames
{
    public interface IMinigame
    {
        void Setup(MiniGameConfig config);
        void BindWorldItemToView(BaseInteractableWorldItem worldItem);
    }
}