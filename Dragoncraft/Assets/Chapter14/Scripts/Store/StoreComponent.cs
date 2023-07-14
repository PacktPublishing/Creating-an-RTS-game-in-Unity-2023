using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dragoncraft
{
    public class StoreComponent : MonoBehaviour
    {
        [SerializeField]
        private ResourceStoreData _resourceStoreData;

        [SerializeField]
        private UnitStoreData _unitStoreData;

        [SerializeField]
        private TMP_Text _errorMessage;

        [SerializeField]
        private List<StoreItemComponent> _storeItems = new List<StoreItemComponent>();

        private void OnEnable()
        {
            if (_resourceStoreData != null)
            {
                for (int i = 0; i < _resourceStoreData.Items.Count; i++)
                {
                    InitializeStoreItem(_storeItems[i], _resourceStoreData.Items[i]);
                }
            }

            if (_unitStoreData != null)
            {
                for (int i = 0; i < _unitStoreData.Items.Count; i++)
                {
                    InitializeStoreItem(_storeItems[i], _unitStoreData.Items[i]);
                }
            }

            _errorMessage.text = string.Empty;
        }

        private void OnDisable()
        {
            foreach (StoreItemComponent storeItem in _storeItems)
            {
                storeItem.gameObject.SetActive(false);
            }
        }

        private void InitializeStoreItem(StoreItemComponent component, StoreItem storeItem)
        {
            component.Initialize(storeItem, PurchaseCallback);
            component.gameObject.SetActive(true);
        }

        private void PurchaseCallback(bool success, string errorMessage)
        {
            if (success)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _errorMessage.text = errorMessage;
            }
        }
    }
}