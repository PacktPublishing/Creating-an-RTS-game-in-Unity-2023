namespace Dragoncraft
{
    public class CollectCommand : ICommand
    {
        public void Execute()
        {
            MessageQueueManager.Instance.SendMessage(new ActionCommandMessage { Action = ActionType.Collect });
        }
    }
}
