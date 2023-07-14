using UnityEngine;

namespace Dragoncraft
{
    public class FireballSpawner : BaseSpawner
    {
        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<FireballSpawnMessage>(OnFireballSpawned);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<FireballSpawnMessage>(OnFireballSpawned);
        }

        private void OnFireballSpawned(FireballSpawnMessage message)
        {
            GameObject fireball = SpawnObject();
            fireball.SetLayerMaskToAllChildren("Unit");

            ProjectileComponent projectile = fireball.GetComponent<ProjectileComponent>();
            projectile.Setup(message.Position, message.Rotation, message.Damage);
        }
    }
}