using System;

namespace Dragoncraft
{
    [Flags]
    [Serializable]
    public enum ActionType
    {
        None = 0,
        Attack = 1,
        Defense = 2,
        Move = 4,
        Collect = 8,
        Build = 16,
        Upgrade = 32
    }
}
