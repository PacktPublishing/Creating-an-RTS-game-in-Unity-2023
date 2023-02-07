using System;
using UnityEngine;

namespace Dragoncraft
{
    [RequireComponent(typeof(BoxCollider), typeof(Animator), typeof(Rigidbody))]
    public class UnitComponent : MonoBehaviour
    {
        public string ID;
        public UnitType Type;
        public int Level;
        public float LevelMultiplier;
        public float Health;
        public float Attack;
        public float Defense;
        public float WalkSpeed;
        public float AttackSpeed;
        public Color SelectedColor;
        public Color OriginalColor;
        public float AttackRange;
        public ActionType Actions;

        private Animator _animator;
        private Renderer _renderer;
        private Vector3 _movePosition;
        private bool _shouldMove;
        private bool _shouldAttack;
        private float _attackCooldown;
        private ActionType _action;
        private UnitData _unitData;
        private float _minDistance = 0.5f;

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _animator = GetComponent<Animator>();
            _action = ActionType.Move;
        }

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
            ID = Guid.NewGuid().ToString();
            Type = unitData.Type;
            Level = unitData.Level;
            LevelMultiplier = unitData.LevelMultiplier;
            Health = unitData.Health;
            Attack = unitData.Attack;
            Defense = unitData.Defense;
            WalkSpeed = unitData.WalkSpeed;
            AttackSpeed = unitData.AttackSpeed;
            SelectedColor = unitData.SelectedColor;
            OriginalColor = unitData.OriginalColor;
            AttackRange = unitData.AttackRange;
            Actions = unitData.Actions;

            _unitData = unitData;

            EnableMovement(false);
        }

        public void Selected(bool selected)
        {
            if (_renderer == null)
            {
                return;
            }

            Material[] materials = _renderer.materials;
            foreach (Material material in materials)
            {
                if (selected)
                {
                    material.SetColor("_EmissionColor", SelectedColor * 0.5f);
                }
                else
                {
                    material.SetColor("_EmissionColor", OriginalColor);
                }
            }
        }

        public void MoveTo(Vector3 position)
        {
            transform.LookAt(position);
            _movePosition = position;

            EnableMovement(true);
        }

        private void Update()
        {
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
                _animator.Play(_unitData.GetAnimationState(UnitAnimationState.Move));
            }
            else
            {
                _animator.Play(_unitData.GetAnimationState(UnitAnimationState.Idle));
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
                MessageQueueManager.Instance.SendMessage(new FireballSpawnMessage { Position = transform.position, Rotation = transform.rotation });
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
                _animator.Play(_unitData.GetAnimationState(endState));
                _shouldMove = false;
                _shouldAttack = true;
                return;
            }

            UpdatePosition();
        }

        protected virtual void UpdatePosition()
        {
            Vector3 direction = (_movePosition - transform.position).normalized;
            transform.position += direction * Time.deltaTime * WalkSpeed;
        }

        protected Vector3 GetFinalPosition()
        {
            return _movePosition;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"name: {collision.gameObject.name} tag: {collision.gameObject.tag}");

            if (!collision.gameObject.CompareTag("Plane"))
            {
                _animator.Play(_unitData.GetAnimationState(UnitAnimationState.Idle));
                _shouldMove = false;
            }
        }
    }
}