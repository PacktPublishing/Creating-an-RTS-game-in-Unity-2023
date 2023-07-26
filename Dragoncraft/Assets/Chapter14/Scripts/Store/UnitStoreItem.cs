using System;

namespace Dragoncraft
{
    [Serializable]
    public class UnitStoreItem : StoreItem
    {
        public UnitType Unit;
        public bool IsUpgrade;
    }
}