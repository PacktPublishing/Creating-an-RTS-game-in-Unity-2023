using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    [CreateAssetMenu(menuName = "Dragoncraft/New Enemy Group")]
    public class EnemyGroupData : ScriptableObject
    {
        public string Name;
        public List<EnemyData> Enemies = new List<EnemyData>();
    }
}
