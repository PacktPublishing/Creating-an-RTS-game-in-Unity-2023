using UnityEngine;

namespace Dragoncraft
{
    public class ResourceCollectionComponent : ResourceProductionComponent
    {
        protected override void Start()
        {
            base.Start();
            _productionType = ResourceProductionType.Manual;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<UnitComponent>(out var unit))
            {
                if (unit.GetActionType() == ActionType.Collect)
                {
                    _productionType = ResourceProductionType.Automatic;
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<UnitComponent>(out var unit))
            {
                _productionType = ResourceProductionType.Manual;
            }
        }
    }
}
