using UnityEditor;
using UnityEngine;

namespace Dragoncraft
{
    public static class EnemyDebugger
    {
        [MenuItem("Dragoncraft/Debug/Enemy/Spawn Orc", priority = 0)]
        private static void SpawnOrc()
        {
            MessageQueueManager.Instance.SendMessage(new BasicOrcSpawnMessage() { SpawnPoint = new Vector3(-6, 0, 0) });
        }

        [MenuItem("Dragoncraft/Debug/Enemy/Spawn Golem", priority = 1)]
        private static void SpawnGolem()
        {
            MessageQueueManager.Instance.SendMessage(new BasicGolemSpawnMessage() { SpawnPoint = new Vector3(6, 0, 0) });
        }

        [MenuItem("Dragoncraft/Debug/Enemy/Spawn Red Dragon", priority = 2)]
        private static void SpawnRedDragon()
        {
            MessageQueueManager.Instance.SendMessage(new RedDragonSpawnMessage() { SpawnPoint = new Vector3(0, 0, 6) });
        }

        [MenuItem("Dragoncraft/Debug/Enemy/Spawn All", priority = 3)]
        private static void SpawnAll()
        {
            SpawnOrc();
            SpawnGolem();
            SpawnRedDragon();
        }
    }
}
