using UnityEngine;

namespace Dragoncraft
{
    public class FireballSpawnMessage : IMessage
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public float Damage;
        public bool IsTower;
    }
}
