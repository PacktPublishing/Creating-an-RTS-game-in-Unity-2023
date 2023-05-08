using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dragoncraft
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _miniMapCameraPrefab;

        [SerializeField]
        private GameObject _fog;

        public List<GameObject> Units { private set; get; }
        public static LevelManager Instance { private set; get; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            Units = new List<GameObject>();
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
    }
}