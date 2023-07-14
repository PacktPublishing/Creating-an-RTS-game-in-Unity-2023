using System;

namespace Dragoncraft
{
    [Serializable]
    public class InventoryData
    {
        public int Gold = 0;
        public int Wood = 0;
        public int Food = 0;
        public int UpgradedGold = 1;
        public int UpgradedWood = 1;
        public int UpgradedFood = 1;
        public int UpgradedWarrior = 1;
        public int UpgradedMage = 1;
    }
}
