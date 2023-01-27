using System;
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

            UnitComponent unit = warrior.GetComponent<UnitComponent>();
            if (unit == null)
            {
                unit = warrior.AddComponent<UnitComponent>();
            }

            unit.CopyData(_unitData);
        }
    }
}