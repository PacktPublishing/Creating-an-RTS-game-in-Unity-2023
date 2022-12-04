using TMPro;
using UnityEngine;

namespace Dragoncraft
{
    public class ResourceUpdater : MonoBehaviour
    {
        [SerializeField]
        private ResourceType _type;

        [SerializeField]
        private TMP_Text _value;

        private int _currentValue;

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<UpdateResourceMessage>(OnResourceUpdated);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<UpdateResourceMessage>(OnResourceUpdated);
        }

        private void Awake()
        {
            if(_value == null)
            {
                Debug.LogError("Missing TMP_Text variable on ResourceUpdater script");
                return;
            }
            UpdateValue();
        }

        private void OnResourceUpdated(UpdateResourceMessage message)
        {
            if (_type == message.Type)
            {
                _currentValue += message.Amount;
                UpdateValue();
            }
        }

        private void UpdateValue()
        {
            _value.text = $"{_type}: {_currentValue}";
        }
    }
}