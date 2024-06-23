using System;
using Sources.Modules.Particles.Scripts;
using Sources.Modules.Player.Scripts.Animation;
using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class Mage : MonoBehaviour
    {
        [SerializeField] private ParticleSpawner _particleSpawner;

        private const float MinDamageScaler = 1;
        private Vector3 _startPosition;

        private Animator _animator;
        private float _maxHealth;
        private float _currentHealth;
        private PlayerSound _sound;

        public event Action Died;
        public event Action<float> HealthChanged;
        public event Action<float> MaxHealthIncreased;

        public float DamageScaler { get; private set; }

        public void Init(PlayerSound playerSound)
        {
            _sound = playerSound;
            _startPosition = transform.position;
        }

        private void Awake()
        {
            DamageScaler = MinDamageScaler;

            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;

            MaxHealthIncreased?.Invoke(_maxHealth);
            HealthChanged?.Invoke(_currentHealth);
        }
        
        public void TryTakeDamage(float damage)
        {
            if (damage > 0 && _currentHealth > 0)
            {
                _animator.Play(PlayerAnimator.States.Hit);
                _sound.DamagedPlay(transform.position);

                _currentHealth -= damage;
                _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

                HealthChanged?.Invoke(_currentHealth);

                if (_currentHealth <= 0) 
                    Die();
            }
        }

        public void UpdateCurrentHealth() 
        {
            _currentHealth = _maxHealth;
            
            HealthChanged?.Invoke(_currentHealth);
        }

        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;

            MaxHealthIncreased?.Invoke(_maxHealth);
        }

        public void OnChangeDamageScaler(float damageScaler)
        {
            DamageScaler = Mathf.Clamp(damageScaler, MinDamageScaler, float.MaxValue);
        }

        public void SetStartPosition() => transform.position = _startPosition;

        private void Die()
        {
            _sound.DiePlay(transform.position);
            _particleSpawner.SpawnParticle(ParticleType.MageDied, transform.position);
            Died?.Invoke();
        }
    }
}