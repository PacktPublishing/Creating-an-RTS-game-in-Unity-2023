using UnityEngine;

namespace Dragoncraft
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileComponent : MonoBehaviour
    {
        [SerializeField]
        private float _timeToLive;

        [SerializeField]
        private float _speed;

        private float _countdown;
        private Rigidbody _rigidbody;

        public float Damage;
        public bool IsTower;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _countdown -= Time.deltaTime;
            if (_countdown <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        public void Setup(Vector3 position, Quaternion rotation, float damage, bool isTower)
        {
            transform.position = position;
            transform.rotation = rotation;
            Damage = damage;
            IsTower = isTower;

            _countdown = _timeToLive;
            _rigidbody.velocity = transform.rotation * Vector3.forward * _speed;
        }
    }
}
