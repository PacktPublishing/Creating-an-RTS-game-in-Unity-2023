namespace Dragoncraft
{
    public class AttackCommand : ICommand
    {
        public void Execute()
        {
            MessageQueueManager.Instance.SendMessage(new ActionCommandMessage { Action = ActionType.Attack });
        }
    }
}
