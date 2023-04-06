﻿using UnityEngine;

namespace Dragoncraft
{
    public class UnitComponent : BaseCharacter
    {
        public UnitType Type;
        public int Level;
        public float LevelMultiplier;
        public ActionType Actions;

        private Vector3 _movePosition;
        private bool _shouldMove;
        private bool _shouldAttack;
        private float _attackCooldown;
        private UnitData _unitData;
        private float _minDistance = 0.5f;

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<ActionCommandMessage>(OnActionCommandReceived);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<ActionCommandMessage>(OnActionCommandReceived);
        }

        private void OnActionCommandReceived(ActionCommandMessage message)
        {
            _action = message.Action;
            _shouldAttack = false;
        }

        public void CopyData(UnitData unitData)
        {
            CopyBaseData(unitData);
            Type = unitData.Type;
            Level = unitData.Level;
            LevelMultiplier = unitData.LevelMultiplier;
            Actions = unitData.Actions;

            _unitData = unitData;
            _action = ActionType.Move;

            EnableMovement(false);
        }

        public void MoveTo(Vector3 position)
        {
            transform.LookAt(position);
            _movePosition = position;

            EnableMovement(true);
        }

        private void Update()
        {
            if (IsDead)
            {
                return;
            }

            switch (_action)
            {
                case ActionType.Attack:
                    UpdateAttack();
                    break;
                case ActionType.Defense:
                    UpdateDefense();
                    break;
                case ActionType.Move:
                    UpdateMovement();
                    break;
                case ActionType.Collect:
                    UpdateCollect();
                    break;
                case ActionType.Build:
                case ActionType.Upgrade:
                case ActionType.None:
                default:
                    EnableMovement(false);
                    break;
            }
        }

        private void EnableMovement(bool enabled)
        {
            if (enabled)
            {
                PlayAnimation(UnitAnimationState.Move);
            }
            else
            {
                PlayAnimation(UnitAnimationState.Idle);
            }
            _shouldMove = enabled;
        }

        private void UpdateAttack()
        {
            UnitAnimationState attackState = (UnityEngine.Random.value < 0.5f) ? UnitAnimationState.Attack01 : UnitAnimationState.Attack02;
            UpdatePosition(_minDistance + AttackRange, attackState);

            if (!_shouldAttack || AttackRange <= 0)
            {
                return;
            }

            _attackCooldown -= Time.deltaTime;
            if (_attackCooldown < 0)
            {
                MessageQueueManager.Instance.SendMessage(new FireballSpawnMessage
                {
                    Position = transform.position,
                    Rotation = transform.rotation,
                    Damage = Attack
                });
                _attackCooldown = AttackSpeed;
            }
        }

        private void UpdateDefense()
        {
            UpdatePosition(_minDistance, UnitAnimationState.Defense);
        }

        private void UpdateMovement()
        {
            UpdatePosition(_minDistance, UnitAnimationState.Idle);
        }

        private void UpdateCollect()
        {
            UpdatePosition(_minDistance, UnitAnimationState.Collect);
        }

        private void UpdatePosition(float range, UnitAnimationState endState)
        {
            if (!_shouldMove)
            {
                return;
            }

            if (Vector3.Distance(transform.position, _movePosition) < range)
            {
                PlayAnimation(endState);
                StopMovingAndAttack();
                return;
            }

            UpdatePosition();
        }

        protected virtual void UpdatePosition()
        {
            Vector3 direction = (_movePosition - transform.position).normalized;
            transform.position += direction * Time.deltaTime * WalkSpeed;
        }

        protected virtual void StopMovingAndAttack()
        {
            _shouldMove = false;
            _shouldAttack = true;
        }

        protected Vector3 GetFinalPosition()
        {
            return _movePosition;
        }

        protected override void UpdateState(ActionType action)
        {
            base.UpdateState(action);

            switch (action)
            {
                case ActionType.Attack:
                    EnableMovement(false);
                    UnitAnimationState attackState = (UnityEngine.Random.value < 0.5f) ? UnitAnimationState.Attack01 : UnitAnimationState.Attack02;
                    PlayAnimation(attackState);
                    break;
                case ActionType.Move:
                    EnableMovement(true);
                    break;
                case ActionType.None:
                    _movePosition = transform.position;
                    break;
                default:
                    break;
            }
        }

        protected override void PlayAnimation(UnitAnimationState animationState)
        {
            _animator.Play(_unitData.GetAnimationState(animationState));
        }
    }
}