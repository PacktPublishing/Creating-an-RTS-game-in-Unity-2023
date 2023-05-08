using UnityEngine;

namespace Dragoncraft
{
    public class BasicWarriorSpawner : BaseSpawner
    {
        [SerializeField]
        private UnitData _unitData;

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<BasicWarriorSpawnMessage>(OnBasicWarriorSpawned);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<BasicWarriorSpawnMessage>(OnBasicWarriorSpawned);
        }

        private void OnBasicWarriorSpawned(BasicWarriorSpawnMessage message)
        {
            GameObject warrior = SpawnObject();
            warrior.SetLayerMaskToAllChildren("Unit");

            UnitComponentNavMesh unit = warrior.GetComponent<UnitComponentNavMesh>();
            if (unit == null)
            {
                unit = warrior.AddComponent<UnitComponentNavMesh>();
            }

            unit.CopyData(_unitData);

            LevelManager.Instance.Units.Add(warrior);
        }
    }
}