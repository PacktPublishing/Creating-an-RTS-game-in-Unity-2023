using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Dragoncraft
{
    public class InventoryManager : IDisposable
    {
        private static string _fileName = "inventory.data";
        private string _filePath = $"{Application.persistentDataPath}/{_fileName}";
        private InventoryData _inventory;

        public InventoryManager()
        {
            LoadData();
        }

        public void Dispose()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        private void SaveData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(_filePath);
            formatter.Serialize(file, _inventory);
            file.Close();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(_filePath, FileMode.Open);

                _inventory = (InventoryData)formatter.Deserialize(file);
                file.Close();
            }
            else
            {
                _inventory = new InventoryData();
            }
        }

        public void UpdateResource(ResourceType type, int amount)
        {
            switch (type)
            {
                case ResourceType.Gold:
                    _inventory.Gold += amount;
                    break;
                case ResourceType.Wood:
                    _inventory.Wood += amount;
                    break;
                case ResourceType.Food:
                    _inventory.Food += amount;
                    break;
                default:
                    break;
            }

            SaveData();
        }

        public int GetResource(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Gold:
                    return _inventory.Gold;
                case ResourceType.Wood:
                    return _inventory.Wood;
                case ResourceType.Food:
                    return _inventory.Food;
                default:
                    return 0;
            }
        }

        public void UpdateResourceUpgrade(ResourceType type, int level)
        {
            switch (type)
            {
                case ResourceType.Gold:
                    _inventory.UpgradedGold += level;
                    break;
                case ResourceType.Wood:
                    _inventory.UpgradedWood += level;
                    break;
                case ResourceType.Food:
                    _inventory.UpgradedFood += level;
                    break;
                default:
                    break;
            }

            SaveData();
        }

        public int GetResourceUpgrade(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Gold:
                    return _inventory.UpgradedGold;
                case ResourceType.Wood:
                    return _inventory.UpgradedWood;
                case ResourceType.Food:
                    return _inventory.UpgradedFood;
                default:
                    return 0;
            }
        }

        public void UpdateUnitUpgrade(UnitType type, int level)
        {
            switch (type)
            {
                case UnitType.Warrior:
                    _inventory.UpgradedWarrior += level;
                    break;
                case UnitType.Mage:
                    _inventory.UpgradedMage += level;
                    break;
                default:
                    break;
            }

            SaveData();
        }

        public int GetUnitUpgrade(UnitType type)
        {
            switch (type)
            {
                case UnitType.Warrior:
                    return _inventory.UpgradedWarrior;
                case UnitType.Mage:
                    return _inventory.UpgradedMage;
                default:
                    return 0;
            }
        }
    }
}
