using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    [CreateAssetMenu(menuName = "Dragoncraft/New Level")]
    public class LevelData : ScriptableObject
    {
        public List<LevelSlot> Slots = new List<LevelSlot>();
        public int Columns;
        public int Rows;
        public LevelConfiguration Configuration;
        public List<EnemyGroupConfiguration> EnemyGroups = new List<EnemyGroupConfiguration>();
    }
}