using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dragoncraft
{
    public class StoreItemComponent : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private TMP_Text _description;

        private StoreItem _storeItem;
        private Action<bool, string> _callback;

        public void Initialize(StoreItem storeItem, Action<bool, string> callback)
        {
            _storeItem = storeItem;
            _callback = callback;
            _image.sprite = storeItem.Image;
            _description.text = $"{storeItem.Description}\n" +
                $"Price: {storeItem.PriceGold} Gold";
            if (storeItem.PriceResource > 0)
            {
                _description.text += $" + {storeItem.PriceResource} " +
                    $"{storeItem.CurrencyResource}";
            }
        }

        public void OnClick()
        {
            if (LevelManager.Instance.GetResource(_storeItem.CurrencyResource) < _storeItem.PriceResource)
            {
                _callback(false, $"Low balance of " +
                    $"{_storeItem.CurrencyResource}. " +
                    $"Required: {_storeItem.PriceResource}");
                return;
            }

            if (LevelManager.Instance.GetResource(ResourceType.Gold) < _storeItem.PriceGold)
            {
                _callback(false, $"Low balance of Gold. " +
                    $"Required: {_storeItem.PriceGold}");
                return;
            }

            UpdateInventory();
            UpgradeResource();
            UpgradeUnit();
            SpawnUnit();

            _callback(true, null);
        }

        private void UpdateInventory()
        {
            LevelManager.Instance.UpdateResource(_storeItem.CurrencyResource, -_storeItem.PriceResource);
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = _storeItem.CurrencyResource });

            LevelManager.Instance.UpdateResource(ResourceType.Gold, -_storeItem.PriceGold);
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Gold });
        }

        private void UpgradeResource()
        {
            if (_storeItem.GetType() != typeof(ResourceStoreItem))
            {
                return;
            }

            ResourceStoreItem item = (ResourceStoreItem)_storeItem;
            MessageQueueManager.Instance.SendMessage(new UpgradeResourceMessage { Type = item.Resource });
        }

        private void UpgradeUnit()
        {
            if (_storeItem.GetType() != typeof(UnitStoreItem))
            {
                return;
            }

            UnitStoreItem item = (UnitStoreItem)_storeItem;
            if (!item.IsUpgrade)
            {
                return;
            }

            MessageQueueManager.Instance.SendMessage(new UpgradeUnitMessage { Type = item.Unit });
        }

        private void SpawnUnit()
        {
            if (_storeItem.GetType() != typeof(UnitStoreItem))
            {
                return;
            }

            UnitStoreItem item = (UnitStoreItem)_storeItem;
            if (item.IsUpgrade)
            {
                return;
            }

            if (item.Unit == UnitType.Warrior)
            {
                MessageQueueManager.Instance.SendMessage(new BasicWarriorSpawnMessage());
            }
            else if (item.Unit == UnitType.Mage)
            {
                MessageQueueManager.Instance.SendMessage(new BasicMageSpawnMessage());
            }
            else if (item.Unit == UnitType.Tower)
            {
                LevelManager.Instance.AddTower(item.Prefab);
            }
        }
    }
}