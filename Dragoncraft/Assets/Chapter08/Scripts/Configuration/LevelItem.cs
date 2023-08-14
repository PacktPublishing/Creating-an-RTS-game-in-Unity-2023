using System;
using UnityEngine;

namespace Dragoncraft
{
    [Serializable]
    public class LevelItem
    {
        public LevelItemType Type;
        public GameObject Prefab;
        public LevelItemCollisionType CollisionType;
    }
}