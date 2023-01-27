using UnityEngine;

namespace Dragoncraft
{
    public class BasicMageSpawner : BaseSpawner
    {
        [SerializeField]
        private UnitData _unitData;

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<BasicMageSpawnMessage>(OnBasicMageSpawned);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<BasicMageSpawnMessage>(OnBasicMageSpawned);
        }

        private void OnBasicMageSpawned(BasicMageSpawnMessage message)
        {
            GameObject mage = SpawnObject();
            mage.SetLayerMaskToAllChildren("Unit");

            UnitComponent unit = mage.GetComponent<UnitComponent>();
            if (unit == null)
            {
                unit = mage.AddComponent<UnitComponent>();
            }

            unit.CopyData(_unitData);
        }
    }
}