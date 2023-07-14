using UnityEngine;

namespace Dragoncraft
{
    public class ResourceProductionComponent : MonoBehaviour
    {
        [SerializeField]
        private ResourceData _resource;

        protected int _productionPerSecond;
        protected int _productionLevel;
        protected ResourceType _type;
        protected ResourceProductionType _productionType;

        private float _counter;

        protected virtual void Start()
        {
            _productionPerSecond = _resource.ProductionPerSecond;
            _productionLevel = _resource.ProductionLevel;
            _type = _resource.Type;
            _productionType = ResourceProductionType.Automatic;
        }

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<UpgradeResourceMessage>(OnResourceUpgraded);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<UpgradeResourceMessage>(OnResourceUpgraded);
        }

        private void OnResourceUpgraded(UpgradeResourceMessage message)
        {
            if (_type == message.Type)
            {
                _productionLevel++;
            }
        }

        private void Update()
        {
            if (_productionType == ResourceProductionType.Manual)
            {
                return;
            }

            _counter += Time.deltaTime;
            if (_counter > 1)
            {
                _counter = 0;
                ProduceResource();
            }
        }

        private void ProduceResource()
        {
            UpdateResourceMessage message = new UpdateResourceMessage
            {
                Amount = GetProducedAmount(),
                Type = _type
            };
            MessageQueueManager.Instance.SendMessage(message);
        }

        private int GetProducedAmount()
        {
            return _productionPerSecond * _productionLevel;
        }
    }
}
