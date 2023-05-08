using System.Threading;
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

            UnitComponentNavMesh unit = mage.GetComponent<UnitComponentNavMesh>();
            if (unit == null)
            {
                unit = mage.AddComponent<UnitComponentNavMesh>();
            }

            unit.CopyData(_unitData);
        }
    }
}