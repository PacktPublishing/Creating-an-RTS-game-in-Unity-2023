using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    [CreateAssetMenu(menuName = "Dragoncraft/New Objective")]
    public class ObjectiveData : ScriptableObject
    {
        public string Description;
        public int TimeInSeconds;
        public List<ResourceObjective> Resources = new List<ResourceObjective>();
        public List<EnemyObjective> Enemies = new List<EnemyObjective>();
    }
}