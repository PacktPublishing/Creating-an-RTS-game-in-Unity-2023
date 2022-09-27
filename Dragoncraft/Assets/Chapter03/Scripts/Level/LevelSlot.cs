using System;
using UnityEngine;

namespace Dragoncraft
{
    [Serializable]
    public class LevelSlot
    {
        public LevelItemType ItemType;
        public Vector2Int Coordinates;

        public LevelSlot(LevelItemType itemType, Vector2Int coordinates)
        {
            ItemType = itemType;
            Coordinates = coordinates;
        }
    }
}