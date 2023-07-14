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
    }
}
