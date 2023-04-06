using UnityEngine;
using UnityEngine.AI;

namespace Dragoncraft
{
    [RequireComponent(typeof(NavMeshAgent), typeof(CollisionComponent))]
    public class UnitComponentNavMesh : UnitComponent
    {
        private NavMeshAgent _agent;
        private CollisionComponent _collisionComponent;

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

        protected override void UpdatePosition()
        {
            _agent.isStopped = false;
            _agent.destination = GetFinalPosition();
        }

        protected override void StopMovingAndAttack()
        {
            base.StopMovingAndAttack();
            _agent.isStopped = true;
        }

        private void OnStartAttacking(Transform target)
        {
            transform.LookAt(target.position);
            _agent.isStopped = true;
            UpdateState(ActionType.Attack);
        }

        private void OnStopAttacking(Transform target, bool opponentIsDead)
        {
            if (IsDead)
            {
                return;
            }

            UpdateState(ActionType.None);
        }
    }
}
