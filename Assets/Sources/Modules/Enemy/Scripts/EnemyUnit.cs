using System;
using UnityEngine;
using Pathfinding;
using Sources.Modules.Particles.Scripts;

namespace Sources.Modules.Enemy
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(EnemyAttack))]
    [RequireComponent(typeof(EnemySound))]
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _maxHealth;
        [SerializeField] private AIDestinationSetter _destinationSetter;
        [SerializeField] private ParticleType _diedType;

        private const float AddMaxHealth = 0.1f;

        private float _currentHealth;
        private EnemySound _sound;
        private EnemyAttack _attack;
        private ParticleSpawner _particleSpawner;
        
        public event Action<EnemyUnit> Died;

        public CapsuleCollider2D Collider2D { get; private set; }
        
        public int CurrentLevel { get; private set; }
        public bool IsDie => _currentHealth <= 0;
        public EnemyType EnemyType => _enemyType;

        public void Init(EnemySound sound)
        {
            _sound = sound;
            Collider2D = GetComponent<CapsuleCollider2D>();
            _attack = GetComponent<EnemyAttack>();
            CurrentLevel = 1;
        }

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        public void SetParticleSpawner(ParticleSpawner particleSpawner)
        {
            _particleSpawner = particleSpawner;
            gameObject.GetComponent<EnemyAttack>().SetParticleSpawner(particleSpawner);
        }
        
        public void SetTarget(Transform target)
        {
            _destinationSetter.target = target;
        }
        
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _sound.PlayDamaged(transform.position);
            
            TryDie();
        }

        public void AddLevels(int wave)
        {
            _attack.AddLevel(wave, CurrentLevel);
            
            while (CurrentLevel < wave)
            {
                _maxHealth += AddMaxHealth;
                CurrentLevel++;
            }
        }

        private void TryDie()
        {
            if (_currentHealth <= 0)
            {
                _particleSpawner.SpawnParticle(_diedType, transform.position);
                Died?.Invoke(this);
                
                _sound.PlayDie(transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
