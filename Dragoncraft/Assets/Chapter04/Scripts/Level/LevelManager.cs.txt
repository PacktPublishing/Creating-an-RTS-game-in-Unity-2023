using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dragoncraft
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _miniMapCameraPrefab;

        private void Start()
        {
            // Instantiates the mini map on the current scene
            Instantiate(_miniMapCameraPrefab);

            // Loads the "GameUI" scene additively on top of the current scene
            SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
        }
    }
}