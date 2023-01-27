using UnityEngine;

namespace Dragoncraft
{
    public static class GameObjectExtension
    {
        public static void SetLayerMaskToAllChildren(this GameObject item, string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            item.layer = layer;
            foreach (Transform child in item.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = layer;
            }
        }
    }
}
