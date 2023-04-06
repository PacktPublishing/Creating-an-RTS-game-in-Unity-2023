using UnityEngine;
using UnityEngine.AI;

namespace Dragoncraft
{
    public class DeadComponent : MonoBehaviour
    {
        private float _timeToLive = 5;
        private float _counter;

        private void Update()
        {
            _counter += Time.deltaTime;
            if (_counter > _timeToLive)
            {
                RemoveComponents();
                ResetComponents();
                gameObject.SetActive(false);
                Destroy(this);
                return;
            }
        }

        private void ResetComponents()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        private void RemoveComponents()
        {
            BaseCharacter character = GetComponent<BaseCharacter>();
            Destroy(character);

            CollisionComponent collision = GetComponent<CollisionComponent>();
            Destroy(collision);

            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            Destroy(navMeshAgent);

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            Destroy(rigidbody);

            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            Destroy(sphereCollider);
        }
    }
}
