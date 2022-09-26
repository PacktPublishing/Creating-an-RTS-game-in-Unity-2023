using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    [CreateAssetMenu(menuName = "Dragoncraft/New Configuration")]
    public class LevelConfiguration : ScriptableObject
    {
        public List<LevelItem> LevelItems = new List<LevelItem>();

        public LevelItem FindByType(LevelItemType type)
        {
            return LevelItems.Find(item => item.Type == type);
        }
    }
}