using UnityEngine;

namespace Dragoncraft
{
    [CreateAssetMenu(menuName = "Dragoncraft/New Resource")]
    public class ResourceData : ScriptableObject
    {
        public int ProductionPerSecond;
        public int ProductionLevel;
        public ResourceType Type;
    }
}