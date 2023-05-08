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
        private int _currentPoint;
        private Vector3 _startPosition;
        private Vector3[] _points = new Vector3[2] { new Vector3(-10, 0, 0), new Vector3(10, 0, 0) };

        private void Start()
        {
            _collisionComponent = GetComponent<CollisionComponent>();
            _collisionComponent.Initialize(this);
            _collisionComponent.OnStartAttacking += OnStartAttacking;
            _collisionComponent.OnStopAttacking += OnStopAttacking;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = WalkSpeed + UnityEngine.Random.Range(0.1f, 0.5f);

            _currentPoint = UnityEngine.Random.Range(0, _points.Length);
            _startPosition = transform.position;
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
            if (IsDead)
            {
                return;
            }

            if (_targetToFollow != null)
            {
                // Chase the unit
                if (Vector3.Distance(transform.position, _targetToFollow.position) < WalkSpeed)
                {
                    return;
                }
                _agent.destination = _targetToFollow.position;
            }
            else
            {
                // Patrol the area
                if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
                {
                    _agent.destination = _startPosition + _points[_currentPoint];
                    _currentPoint = (_currentPoint + 1) % _points.Length;
                    UpdateState(ActionType.Move);
                }
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

            // Chase the unit
            if (!opponentIsDead)
            {
                transform.LookAt(target.position);
                _targetToFollow = target;
            }

            // Patrol the area (when not chasing)
            _agent.isStopped = false;
            _startPosition = transform.position;
            UpdateState(ActionType.Move);
        }
    }
}
