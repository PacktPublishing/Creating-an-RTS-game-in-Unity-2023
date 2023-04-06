/*
using System;
using UnityEngine;

namespace Dragoncraft
{
    [RequireComponent(typeof(BoxCollider), typeof(Animator))]
    public class EnemyComponent : MonoBehaviour
    {
        public string ID;
        public EnemyType Type;
        public float Health;
        public float Attack;
        public float Defense;
        public float WalkSpeed;
        public float AttackSpeed;
        public Color SelectedColor;

        private Animator _animator;
        private Renderer _renderer;
        private Color _originalColor;
        private EnemyData _enemyData;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _renderer = GetComponentInChildren<Renderer>();
            _originalColor = _renderer.material.color;
        }

        public void CopyData(EnemyData enemyData, Vector3 spawnPoint)
        {
            ID = Guid.NewGuid().ToString();
            Type = enemyData.Type;
            Health = enemyData.Health;
            Attack = enemyData.Attack;
            Defense = enemyData.Defense;
            WalkSpeed = enemyData.WalkSpeed;
            AttackSpeed = enemyData.AttackSpeed;
            SelectedColor = enemyData.SelectedColor;

            _enemyData = enemyData;

            transform.position = spawnPoint;
            _animator.Play(_enemyData.GetAnimationState(UnitAnimationState.Idle));
        }

        private void OnMouseEnter()
        {
            _renderer.material.color = SelectedColor;
        }

        private void OnMouseExit()
        {
            _renderer.material.color = _originalColor;
        }

        public void Selected()
        {
            // Test code for the damage
            TakeDamage(Attack);
        }

        public void TakeDamage(float attack)
        {
            if (Health <= 0)
            {
                return;
            }

            float damage = attack - Defense;
            if (damage > 0)
            {
                Health -= damage;
                // Feedback with the position + 1/4 of the model size as offset
                Vector3 position = transform.position + (_renderer.bounds.size * 0.25f);
                MessageQueueManager.Instance.SendMessage(new DamageFeedbackMessage() { Damage = damage, Position = position });
            }

            if (Health <= 0)
            {
                _animator.Play(_enemyData.GetAnimationState(UnitAnimationState.Death));
            }
        }
    }
}
*/