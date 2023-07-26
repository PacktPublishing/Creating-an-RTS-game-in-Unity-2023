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
        private ObjectPoolComponent _objectPool;

        [SerializeField]
        private GameObject _content;

        private void OnEnable()
        {
            if (_resourceStoreData != null)
            {
                for (int i = 0; i < _resourceStoreData.Items.Count; i++)
                {
                    InitializeStoreItem(_objectPool.GetObject(), _resourceStoreData.Items[i]);
                }
            }

            if (_unitStoreData != null)
            {
                for (int i = 0; i < _unitStoreData.Items.Count; i++)
                {
                    InitializeStoreItem(_objectPool.GetObject(), _unitStoreData.Items[i]);
                }
            }

            _errorMessage.text = string.Empty;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _content.transform.childCount; i++)
            {
                _content.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void InitializeStoreItem(GameObject component, StoreItem storeItem)
        {
            component.transform.SetParent(_content.transform, false);

            StoreItemComponent storeItemComponent = component.GetComponent<StoreItemComponent>();
            storeItemComponent.Initialize(storeItem, PurchaseCallback);
            storeItemComponent.gameObject.SetActive(true);
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