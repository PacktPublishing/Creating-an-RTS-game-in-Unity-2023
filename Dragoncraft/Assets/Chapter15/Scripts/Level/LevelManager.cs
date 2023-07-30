using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Dragoncraft
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _miniMapCameraPrefab;

        [SerializeField]
        private GameObject _fog;

        [SerializeField]
        private ObjectiveData _objectiveData;

        public List<GameObject> Units { private set; get; }
        public static LevelManager Instance { private set; get; }

        private InventoryManager _inventory;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            Units = new List<GameObject>();
            _inventory = new InventoryManager();
            _inventory.UpdateResource(ResourceType.Gold, 100);
        }

        private void Start()
        {
            if (_miniMapCameraPrefab == null || _fog == null)
            {
                Debug.LogError("Missing MiniMapCamera prefab or Fog game object");
                return;
            }

            // Enable the fog game object
            _fog.SetActive(true);

            // Instantiates the mini map on the current scene
            Instantiate(_miniMapCameraPrefab);

            // Loads the "GameUI" scene additively on top of the current scene
            SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
        }

        private void OnDestroy()
        {
            _inventory.Dispose();
        }

        public void UpdateResource(ResourceType type, int amount)
        {
            _inventory.UpdateResource(type, amount);
        }

        public int GetResource(ResourceType type)
        {
            return _inventory.GetResource(type);
        }

        public void AddBuilding(GameObject prefab)
        {
            GameObject building = Instantiate(prefab);
            building.AddComponent<MeshCollider>();
            building.AddComponent<NavMeshObstacle>();
            building.transform.localScale = Vector3.one * 0.5f;
            building.layer = LayerMask.NameToLayer("Resource");
            building.tag = "Building";
        }

        public void AddTower(GameObject prefab)
        {
            Instantiate(prefab);
        }

        public ObjectiveData GetObjectiveData()
        {
            return _objectiveData;
        }
    }
}