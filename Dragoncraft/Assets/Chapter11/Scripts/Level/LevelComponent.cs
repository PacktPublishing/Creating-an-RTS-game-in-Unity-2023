using UnityEngine;
using UnityEngine.AI;

namespace Dragoncraft
{
    public class LevelComponent : MonoBehaviour
    {
        [SerializeField]
        private LevelData _levelData;

        [SerializeField]
        private GameObject _plane;

        private float _distanceBetweenEnemies = 3.0f;

        private void Start()
        {
            // If any of the class variables were not set in the Editor and error is displayed and the Start is skiped 
            if (_levelData == null || _plane == null)
            {
                return;
            }

            // The plane game object always have a Collier component preset (unless manually remove)
            Collider planeCollider = _plane.GetComponent<Collider>();
            // The size of the plane is the boundaries of the collider
            Vector3 planeSize = planeCollider.bounds.size;
            // Settings the start position to the upper left corner sinde the origin (0, 0) is in the middle of the screen
            Vector3 startPosition = new Vector3(-planeSize.x / 2, 0, planeSize.z / 2);

            // The distance of each instantiated prefab is the size of the slot in the map grid
            float offsetX = planeSize.x / _levelData.Columns - 1;
            float offsetZ = planeSize.z / _levelData.Rows - 1;

            Initialize(startPosition, offsetX, offsetZ);
            SpawnEnemyGroups();
        }

        private void Initialize(Vector3 start, float offsetX, float offsetZ)
        {
            foreach (LevelSlot slot in _levelData.Slots)
            {
                // For each slot we try to find the level item based on the type, if it is null we move to the next one
                LevelItem levelItem = _levelData.Configuration.FindByType(slot.ItemType);
                if (levelItem == null)
                {
                    continue;
                }

                // Calculates the X and Z based on the start position, coodinates and offset
                // Note that the values are added to the X because we are moving from left to right horiontally
                // and the values are subtracted from the Z because we are moving from top to bottom vertically
                float x = start.x + (slot.Coordinates.y * offsetX) + offsetX / 2;
                float z = start.z - (slot.Coordinates.x * offsetZ) - offsetZ / 2;
                // The Y is not used since we are instantiating the prefab based on the X and Z position of the grid 
                Vector3 position = new Vector3(x, 0, z);

                // Instantiates the prefab at the desired position with the default rotation (Quaternion.identity)
                // and set the game object with this script as the parent of the new game object
                GameObject item = Instantiate(levelItem.Prefab, position, Quaternion.identity, transform);

                switch (levelItem.CollistionType)
                {
                    case LevelItemCollistionType.Rigidbody:
                        item.AddComponent<BoxCollider>();
                        break;
                    case LevelItemCollistionType.NavMesh:
                        item.AddComponent<NavMeshObstacle>();
                        break;
                    case LevelItemCollistionType.None:
                    default:
                        break;
                }
            }
        }

        private void SpawnEnemyGroups()
        {
            foreach (EnemyGroupConfiguration enemyGroup in _levelData.EnemyGroups)
            {
                SpawnEnemyGroup(enemyGroup);
            }
        }

        private void SpawnEnemyGroup(EnemyGroupConfiguration enemyGroup)
        {
            int rows = Mathf.RoundToInt(Mathf.Sqrt(enemyGroup.Data.Enemies.Count));
            int counter = 0;

            for (int i = 0; i < enemyGroup.Data.Enemies.Count; i++)
            {
                if (i > 0 && (i % rows) == 0)
                {
                    counter++;
                }

                float offsetX = (i % rows) * _distanceBetweenEnemies;
                float offsetZ = counter * _distanceBetweenEnemies;

                Vector3 offset = new Vector3(offsetX, 0, offsetZ);
                Vector3 spawnPoint = enemyGroup.Position + offset;

                SpawnEnemy(enemyGroup.Data.Enemies[i].Type, spawnPoint);
            }
        }

        private void SpawnEnemy(EnemyType enemyType, Vector3 spawnPoint)
        {
            switch (enemyType)
            {
                case EnemyType.Orc:
                    MessageQueueManager.Instance.SendMessage(new BasicOrcSpawnMessage { SpawnPoint = spawnPoint });
                    break;
                case EnemyType.Golem:
                    MessageQueueManager.Instance.SendMessage(new BasicGolemSpawnMessage { SpawnPoint = spawnPoint });
                    break;
                case EnemyType.Dragon:
                    MessageQueueManager.Instance.SendMessage(new RedDragonSpawnMessage { SpawnPoint = spawnPoint });
                    break;
                default:
                    break;
            }
        }
    }
}