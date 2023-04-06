using UnityEngine;
using UnityEngine.AI;

namespace Dragoncraft
{
    [RequireComponent(typeof(NavMeshAgent), typeof(CollisionComponent))]
    public class EnemyComponentNavMesh : EnemyComponent
    {
        private NavMeshAgent _agent;
        private CollisionComponent _collisionComponent;
        private Transform _targetToFollow;

        private void Start()
        {
            _collisionComponent = GetComponent<CollisionComponent>();
            _collisionComponent.Initialize(this);
            _collisionComponent.OnStartAttacking += OnStartAttacking;
            _collisionComponent.OnStopAttacking += OnStopAttacking;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = WalkSpeed;
        }

        private void OnDestroy()
        {
            if (_collisionComponent != null)
            {
                _collisionComponent.OnStartAttacking -= OnStartAttacking;
                _collisionComponent.OnStopAttacking -= OnStopAttacking;
            }
        }

        private void Update()
        {
            if (!IsDead && _targetToFollow != null)
            {
                if (Vector3.Distance(transform.position, _targetToFollow.position) < WalkSpeed)
                {
                    return;
                }
                _agent.destination = _targetToFollow.position;
            }
        }

        private void OnStartAttacking(Transform target)
        {
            transform.LookAt(target.position);
            _agent.isStopped = true;
            _targetToFollow = null;
            UpdateState(ActionType.Attack);
        }

        private void OnStopAttacking(Transform target, bool opponentIsDead)
        {
            if (IsDead)
            {
                return;
            }

            if (!opponentIsDead)
            {
                transform.LookAt(target.position);
                _agent.isStopped = false;
                _targetToFollow = target;
                UpdateState(ActionType.Move);
            }
            else
            {
                UpdateState(ActionType.None);
            }
        }
    }
}
