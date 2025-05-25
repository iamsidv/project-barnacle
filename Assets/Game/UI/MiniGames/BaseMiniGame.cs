namespace Game.UI.Minigames
{
    public abstract class BaseMiniGame
    {
        public abstract void Enter();
        public abstract void Exit();
    }

    public interface ITickable
    {
        void Tick();
    }
}