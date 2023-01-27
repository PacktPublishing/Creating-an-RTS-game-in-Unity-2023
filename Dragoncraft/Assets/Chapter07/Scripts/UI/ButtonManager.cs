using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dragoncraft
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _buttons;

        private void Awake()
        {
            DisableAllButtons();
        }

        private void DisableAllButtons()
        {
            foreach (GameObject button in _buttons)
            {
                button.SetActive(false);
            }
        }

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<UpdateActionsMessage>(OnActionsUpdated);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<UpdateActionsMessage>(OnActionsUpdated);
        }

        private void OnActionsUpdated(UpdateActionsMessage message)
        {
            DisableAllButtons();

            int counter = 0;
            foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
            {
                if (action == ActionType.None)
                {
                    continue;
                }

                if (message.Actions.HasFlag(action))
                {
                    SetButtonType(action, _buttons[counter]);
                    counter++;
                }
            }
        }

        private void SetButtonType(ActionType action, GameObject button)
        {
            ButtonComponent component = button.GetComponent<ButtonComponent>();
            component.Action = action;

            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            text.text = action.ToString();

            button.SetActive(true);
        }
    }
}