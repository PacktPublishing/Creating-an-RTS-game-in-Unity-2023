using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    [CreateAssetMenu(menuName = "Dragoncraft/New Resource Store")]
    public class ResourceStoreData : ScriptableObject
    {
        public List<ResourceStoreItem> Items = new List<ResourceStoreItem>();
    }
}