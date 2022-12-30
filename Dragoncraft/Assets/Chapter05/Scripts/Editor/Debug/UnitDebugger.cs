using UnityEditor;

namespace Dragoncraft
{
    public static class UnitDebugger
    {
        // Creates a shortcute for Ctrl+G (or Cmd+G on macOS)
        [MenuItem("Dragoncraft/Debug/Unit/Spawn Warrior %g")]
        private static void SpawnWarrior()
        {
            MessageQueueManager.Instance.SendMessage(new BasicWarriorSpawnMessage());
        }
    }
}
