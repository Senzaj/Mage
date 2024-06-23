using System.Collections;
using Sources.Modules.Common;
using Sources.Modules.Enemy;
using Sources.Modules.Particles.Scripts;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Common
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField, Range(MinSpeed, MaxSpeed)] private float _speed;
        [SerializeField, Range(MinTimeToDestroy, MaxTimeToDestroy)] private float _timeToDestroy;
        [SerializeField, Range(MinDistanceToLaunch, MaxDistanceToLaunch)] protected float DistanceToLaunch;
        [SerializeField] private float _baseDamage;
        [SerializeField] private SpellType _spellType;
        [SerializeField] protected ParticleType _damagedParticle;
        [SerializeField] protected ParticleType _destroyedParticle;

        protected Coroutine DisablingWork;
        protected ShootPoint ShootPoint;
        protected ParticleSpawner _particleSpawner;

        private const float MinDamageScaler = 1;
        private const int MinSpeed = 1;
        private const int MaxSpeed = 50;
        private const int MinTimeToDestroy = 5;
        private const int MaxTimeToDestroy = 30;
        private const int MinDistanceToLaunch = 1;
        private const int MaxDistanceToLaunch = 100;

        private Rigidbody2D _rigidbody2D;
        private float _currentTimeToDisable;
        private float _damageScaler;

        public float BaseDamage => _baseDamage;
        public SpellType SpellType => _spellType;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _damageScaler = MinDamageScaler;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            bool enemyReceived = other.gameObject.TryGetComponent(out EnemyUnit enemy);
            bool obstacleReceived = other.gameObject.TryGetComponent<Obstacle>(out _);
            
            if (obstacleReceived || enemyReceived)
            {
                gameObject.SetActive(false);
                _particleSpawner.SpawnParticle(_damagedParticle, transform.position);
            }
            
            if (enemyReceived)
            {
                enemy.TakeDamage(GetDamage());
            }
        }

        public void SetParticleSpawner(ParticleSpawner particleSpawner) => _particleSpawner = particleSpawner;
        
        public abstract void TryLaunch(ShootPoint shootPoint, Vector3 position);

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);
        
        public void SetDamageScaler(float damageScaler) => _damageScaler = Mathf.Clamp(damageScaler, MinDamageScaler, float.MaxValue);

        public float GetDamage()
        {
            return _baseDamage * _damageScaler;
        }
        protected IEnumerator ChangingPosition(Vector3 position)
        {
            Vector2 direction = (position - ShootPoint.GetPosition()).normalized;

            while (_currentTimeToDisable > 0)
            {
                _rigidbody2D.velocity = direction * _speed;
                
                yield return null;
            }
        }

        protected IEnumerator Rotating(Transform center, float radius)
        {
            float positionX;
            float positionY;
            float angle = 0;
            
            while (_currentTimeToDisable > 0)
            {
                positionX = center.position.x + Mathf.Cos(angle) * radius;
                positionY = center.position.y + Mathf.Sin(angle) * radius;
                transform.position = new Vector2(positionX, positionY);
                angle = angle + Time.deltaTime * _speed;

                if (angle >= 360f)
                    angle = 0;

                yield return null;
            }
        }

        protected IEnumerator Disabling()
        {
            _currentTimeToDisable = _timeToDestroy;

            while (_currentTimeToDisable > 0)
            {
                _currentTimeToDisable -= Time.deltaTime;

                yield return null;
            }
            
            _particleSpawner.SpawnParticle(_destroyedParticle, transform.position);
            gameObject.SetActive(false);
        }
    }
}