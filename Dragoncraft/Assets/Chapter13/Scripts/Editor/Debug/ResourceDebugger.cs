using UnityEditor;

namespace Dragoncraft
{
    public static class ResourceDebugger
    {
        [MenuItem("Dragoncraft/Debug/Resources/+100 Gold", priority = 0)]
        private static void AddGold()
        {
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Gold, Amount = 100 });
        }

        [MenuItem("Dragoncraft/Debug/Resources/-100 Gold", priority = 1)]
        private static void SubtractGold()
        {
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Gold, Amount = -100 });
        }

        [MenuItem("Dragoncraft/Debug/Resources/+100 Wood", priority = 2)]
        private static void AddWood()
        {
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Wood, Amount = 100 });
        }

        [MenuItem("Dragoncraft/Debug/Resources/-100 Wood", priority = 3)]
        private static void SubtractWood()
        {
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Wood, Amount = -100 });
        }

        [MenuItem("Dragoncraft/Debug/Resources/+100 Food", priority = 4)]
        private static void AddFood()
        {
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Food, Amount = 100 });
        }

        [MenuItem("Dragoncraft/Debug/Resources/-100 Food", priority = 5)]
        private static void SubtractFood()
        {
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Food, Amount = -100 });
        }

        [MenuItem("Dragoncraft/Debug/Resources/Upgrade Gold", priority = 6)]
        private static void UpgradeGold()
        {
            MessageQueueManager.Instance.SendMessage(new UpgradeResourceMessage { Type = ResourceType.Gold });
        }

        [MenuItem("Dragoncraft/Debug/Resources/Upgrade Wood", priority = 7)]
        private static void UpgradeWood()
        {
            MessageQueueManager.Instance.SendMessage(new UpgradeResourceMessage { Type = ResourceType.Wood });
        }

        [MenuItem("Dragoncraft/Debug/Resources/Upgrade Food", priority = 8)]
        private static void UpgradeFood()
        {
            MessageQueueManager.Instance.SendMessage(new UpgradeResourceMessage { Type = ResourceType.Food });
        }

        [MenuItem("Dragoncraft/Debug/Resources/Add 10k of each resource", priority = 9)]
        private static void AddResources()
        {
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Gold, Amount = 10000 });
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Wood, Amount = 10000 });
            MessageQueueManager.Instance.SendMessage(new UpdateResourceMessage { Type = ResourceType.Food, Amount = 10000 });
        }
    }
}
