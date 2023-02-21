using UnityEditor;
using UnityEngine;

namespace Dragoncraft
{
    public static class EnemyDebugger
    {
        [MenuItem("Dragoncraft/Debug/Enemy/Spawn Orc")]
        private static void SpawnOrc()
        {
            MessageQueueManager.Instance.SendMessage(new BasicOrcSpawnMessage() { SpawnPoint = new Vector3(-6, 0, 0) });
        }

        [MenuItem("Dragoncraft/Debug/Enemy/Spawn Golem")]
        private static void SpawnGolem()
        {
            MessageQueueManager.Instance.SendMessage(new BasicGolemSpawnMessage() { SpawnPoint = new Vector3(6, 0, 0) });
        }

        [MenuItem("Dragoncraft/Debug/Enemy/Spawn Red Dragon")]
        private static void SpawnRedDragon()
        {
            MessageQueueManager.Instance.SendMessage(new RedDragonSpawnMessage() { SpawnPoint = new Vector3(0, 0, 6) });
        }

        //[MenuItem("Dragoncraft/Debug/Enemy/Spawn All")]
        //private static void SpawnAll()
        //{
        //    SpawnOrc();
        //    SpawnGolem();
        //    SpawnRedDragon();
        //}
    }
}
