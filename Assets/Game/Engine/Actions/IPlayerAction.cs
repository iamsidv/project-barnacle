namespace Game.Engine.Actions
{
    public interface IPlayerAction
    {
        ActionResult Execute(PlayerContext context);
    }

    public enum ActionResult
    {
        Success,
        Failure,
        InvalidState,
        Exists
    }
}