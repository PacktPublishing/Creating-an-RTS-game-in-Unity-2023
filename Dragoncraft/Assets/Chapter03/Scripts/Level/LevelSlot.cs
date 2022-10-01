using System;
using UnityEngine;

namespace Dragoncraft
{
    [Serializable]
    public class LevelSlot
    {
        public LevelItemType ItemType;
        public Vector2Int Coordinates;

        // This constructor ensures that the object is only created when these two parameters are set
        public LevelSlot(LevelItemType itemType, Vector2Int coordinates)
        {
            ItemType = itemType;
            Coordinates = coordinates;
        }
    }
}