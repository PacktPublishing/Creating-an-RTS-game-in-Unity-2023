using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    public class ObjectPoolComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private int _poolSize;

        [SerializeField]
        private bool _allowCreation;

        [SerializeField]
        private List<GameObject> _gameObjects = new List<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                _gameObjects.Add(CreateItem(false));
            }
        }

        private GameObject CreateItem(bool active)
        {
            GameObject item = Instantiate(_prefab);
            item.transform.SetParent(gameObject.transform);
            item.SetActive(active);
            return item;
        }

        public GameObject GetObject()
        {
            foreach (GameObject item in _gameObjects)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    return item;
                }
            }

            if (_allowCreation)
            {
                GameObject item = CreateItem(true);
                _gameObjects.Add(item);
                return item;
            }

            return null;
        }
    }
}
