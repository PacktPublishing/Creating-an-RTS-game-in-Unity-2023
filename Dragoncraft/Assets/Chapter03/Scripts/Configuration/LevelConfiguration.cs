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
            // Finds the first occurrence of the type in the list or return null otherwise
            return LevelItems.Find(item => item.Type == type);
        }
    }
}