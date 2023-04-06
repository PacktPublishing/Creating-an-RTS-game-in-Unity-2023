using UnityEngine;

namespace Dragoncraft
{
    public class EnemyComponent : BaseCharacter
    {
        public EnemyType Type;

        private EnemyData _enemyData;

        public void CopyData(EnemyData enemyData, Vector3 spawnPoint)
        {
            CopyBaseData(enemyData);
            Type = enemyData.Type;

            _enemyData = enemyData;
            _action = ActionType.None;

            transform.position = spawnPoint;
            PlayAnimation(UnitAnimationState.Idle);
        }

        protected override void UpdateState(ActionType action)
        {
            base.UpdateState(action);

            switch (action)
            {
                case ActionType.Attack:
                    UnitAnimationState attackState = (UnityEngine.Random.value < 0.5f) ? UnitAnimationState.Attack01 : UnitAnimationState.Attack02;
                    PlayAnimation(attackState);
                    break;
                case ActionType.Move:
                    PlayAnimation(UnitAnimationState.Move);
                    break;
                case ActionType.None:
                    PlayAnimation(UnitAnimationState.Idle);
                    break;
                default:
                    break;
            }
        }

        protected override void PlayAnimation(UnitAnimationState animationState)
        {
            _animator.Play(_enemyData.GetAnimationState(animationState));
        }
    }
}
