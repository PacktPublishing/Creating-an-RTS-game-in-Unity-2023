using UnityEngine;

namespace Dragoncraft
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField]
        private LevelData _levelData;

        [SerializeField]
        private GameObject _plane;

        private void Start()
        {
            if (_levelData == null || _plane == null)
            {
                Debug.LogError("Missing LevelData or Plane");
                return;
            }

            Collider planeCollider = _plane.GetComponent<Collider>();

            Vector3 planeSize = planeCollider.bounds.size;

            Vector3 startPosition = new Vector3(-planeSize.x / 2, 0, planeSize.z / 2);

            float offsetX = planeSize.x / _levelData.Columns - 1;

            float offsetZ = planeSize.z / _levelData.Rows - 1;

            Initialize(startPosition, offsetX, offsetZ);
        }

        private void Initialize(Vector3 start, float offsetX, float offsetZ)
        {
            foreach (LevelSlot slot in _levelData.Slots)
            {
                LevelItem levelItem = _levelData.Configuration.FindByType(slot.ItemType);
                if (levelItem == null)
                {
                    continue;
                }

                float x = start.x + (slot.Coordinates.y * offsetX) + offsetX / 2;
                float z = start.z - (slot.Coordinates.x * offsetZ) - offsetZ / 2;
                Vector3 position = new Vector3(x, 0, z);

                Instantiate(levelItem.Prefab, position, Quaternion.identity, transform);
            }
        }
    }
}