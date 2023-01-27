using UnityEngine;

namespace Dragoncraft
{
    public class ButtonComponent : MonoBehaviour
    {
        public ActionType Action;

        private ICommand _command;

        private void Start()
        {
            switch (Action)
            {
                case ActionType.Attack:
                    _command = new AttackCommand();
                    break;
                case ActionType.Defense:
                    _command = new DefenseCommand();
                    break;
                case ActionType.Move:
                    _command = new MoveCommand();
                    break;
                case ActionType.Collect:
                    _command = new CollectCommand();
                    break;
                case ActionType.Build:
                    break;
                case ActionType.Upgrade:
                    break;
                case ActionType.None:
                default:
                    break;
            }
        }

        public void OnClick()
        {
            _command?.Execute();
        }
    }
}