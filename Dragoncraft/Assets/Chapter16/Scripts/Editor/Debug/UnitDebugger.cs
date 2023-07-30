#if UNITY_EDITOR
using UnityEditor;

namespace Dragoncraft
{
    public static class UnitDebugger
    {
        // Creates a shortcute for Ctrl+G (or Cmd+G on macOS)
        [MenuItem("Dragoncraft/Debug/Unit/Spawn Warrior %g", priority = 0)]
        private static void SpawnWarrior()
        {
            MessageQueueManager.Instance.SendMessage(new BasicWarriorSpawnMessage());
        }

        // Creates a shortcute for Ctrl+H (or Cmd+H on macOS)
        [MenuItem("Dragoncraft/Debug/Unit/Spawn Mage %h", priority = 1)]
        private static void SpawnMage()
        {
            MessageQueueManager.Instance.SendMessage(new BasicMageSpawnMessage());
        }

        [MenuItem("Dragoncraft/Debug/Unit/Upgrade Warrior", priority = 2)]
        private static void UpgradeWarrior()
        {
            MessageQueueManager.Instance.SendMessage(new UpgradeUnitMessage { Type = UnitType.Warrior });
        }

        [MenuItem("Dragoncraft/Debug/Unit/Upgrade Mage", priority = 3)]
        private static void UpgradeMage()
        {
            MessageQueueManager.Instance.SendMessage(new UpgradeUnitMessage { Type = UnitType.Mage });
        }
    }
}
#endif
