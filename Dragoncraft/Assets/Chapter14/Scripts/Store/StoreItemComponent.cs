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
                $"Price: {storeItem.PriceGold} Gold + " +
                $"{storeItem.PriceResource} {storeItem.CurrencyResource}";
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
            UpgradeResourceInventory();
            UpgradeUnitInventory();

            _callback(true, null);
        }

        private void UpdateInventory()
        {
            LevelManager.Instance.UpdateResource(_storeItem.CurrencyResource, -_storeItem.PriceResource);
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = _storeItem.CurrencyResource });

            LevelManager.Instance.UpdateResource(ResourceType.Gold, -_storeItem.PriceGold);
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Gold });
        }

        private void UpgradeResourceInventory()
        {
            if (_storeItem.GetType() != typeof(ResourceStoreItem))
            {
                return;
            }

            ResourceStoreItem item = (ResourceStoreItem)_storeItem;
            MessageQueueManager.Instance.SendMessage(new UpgradeResourceMessage { Type = item.Resource });

            int level = LevelManager.Instance.GetResourceUpgrade(item.Resource);
            if (level == 1)
            {
                LevelManager.Instance.AddBuilding(item.Prefab);
            }
            LevelManager.Instance.UpdateResourceUpgrade(item.Resource, 1);
        }

        private void UpgradeUnitInventory()
        {
            if (_storeItem.GetType() != typeof(UnitStoreItem))
            {
                return;
            }

            UnitStoreItem item = (UnitStoreItem)_storeItem;
            LevelManager.Instance.UpdateUnitUpgrade(item.Unit, 1);
            MessageQueueManager.Instance.SendMessage(new UpgradeUnitMessage { Type = item.Unit });

            if (item.Unit == UnitType.Tower)
            {
                LevelManager.Instance.AddTower(item.Prefab);
            }
        }
    }
}