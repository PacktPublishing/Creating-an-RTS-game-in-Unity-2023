using System;
using UnityEngine;

namespace Dragoncraft
{
    [Serializable]
    public class StoreItem
    {
        public string Description;
        public int PriceGold;
        public int PriceResource;
        public ResourceType CurrencyResource;
        public Sprite Image;
        public GameObject Prefab;
    }
}