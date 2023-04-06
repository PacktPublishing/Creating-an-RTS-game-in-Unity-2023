/*
using UnityEngine;
using UnityEngine.AI;

namespace Dragoncraft
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitComponentNavMesh : UnitComponent
    {
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        protected override void UpdatePosition()
        {
            _agent.destination = GetFinalPosition();
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            if (!collision.gameObject.CompareTag("Plane"))
            {
                _agent.isStopped = true;
            }
        }
    }
}
*/