namespace Game.Engine.Actions
{
    public class LogPlayerState : IPlayerAction
    {
        public ActionResult Execute(PlayerContext context)
        {
            context.Player.PrintState();
            return ActionResult.Success;
        }
    }
}