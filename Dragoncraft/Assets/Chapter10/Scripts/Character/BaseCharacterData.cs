using UnityEngine;

namespace Dragoncraft
{
    public class BaseCharacterData : ScriptableObject
    {
        public float Health;
        public float Attack;
        public float Defense;
        public float WalkSpeed;
        public float AttackSpeed;
        public Color SelectedColor;
        public string AnimationStateAttack01;
        public string AnimationStateAttack02;
        public string AnimationStateDefense;
        public string AnimationStateMove;
        public string AnimationStateIdle;
        public string AnimationStateCollect;
        public string AnimationStateDeath;
        public float AttackRange;
        public float ColliderSize;

        public string GetAnimationState(UnitAnimationState animationState)
        {
            switch (animationState)
            {
                case UnitAnimationState.Attack01:
                    return AnimationStateAttack01;
                case UnitAnimationState.Attack02:
                    return AnimationStateAttack02;
                case UnitAnimationState.Defense:
                    return AnimationStateDefense;
                case UnitAnimationState.Move:
                    return AnimationStateMove;
                case UnitAnimationState.Idle:
                    return AnimationStateIdle;
                case UnitAnimationState.Collect:
                    return AnimationStateCollect;
                case UnitAnimationState.Death:
                    return AnimationStateDeath;
                default:
                    return AnimationStateIdle;
            }
        }
    }
}
