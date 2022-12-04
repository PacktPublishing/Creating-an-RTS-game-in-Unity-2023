using System;
using UnityEngine;

namespace Dragoncraft
{
    public class BasicWarriorSpawner : BaseSpawner
    {
        [SerializeField]
        public UnitData _unitData;

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

            UnitComponent unit = warrior.GetComponent<UnitComponent>();
            if (unit == null)
            {
                unit = warrior.AddComponent<UnitComponent>();
            }

            unit.ID = Guid.NewGuid().ToString();
            unit.Type = _unitData.Type;
            unit.Level = _unitData.Level;
            unit.LevelMultiplier = _unitData.LevelMultiplier;
            unit.Health = _unitData.Health;
            unit.Attack = _unitData.Attack;
            unit.Defense = _unitData.Defense;
            unit.WalkSpeed = _unitData.WalkSpeed;
            unit.AttackSpeed = _unitData.AttackSpeed;
        }
    }
}