using Sources.Modules.Enemy;
using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Base.Spells
{
    internal class IndestructibleRotating : Projectile
    {
        [SerializeField] private float _radius;
        [SerializeField] private int _enemyCollisionsNeedToDestroy;

        private int _currentEnemyCollisionsCount = 0;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            bool enemyReceived = other.gameObject.TryGetComponent(out EnemyUnit enemy);

            if (enemyReceived)
            {
                _particleSpawner.SpawnParticle(_damagedParticle, transform.position);
                enemy.TakeDamage(GetDamage());

                _currentEnemyCollisionsCount++;

                if (_currentEnemyCollisionsCount == _enemyCollisionsNeedToDestroy)
                {
                    _particleSpawner.SpawnParticle(_destroyedParticle, transform.position);
                    _currentEnemyCollisionsCount = 0;
                    gameObject.SetActive(false);
                }
            }
        }
        
        public override void TryLaunch(ShootPoint shootPoint, Vector3 position)
        {
            if ((Vector2.Distance(position, shootPoint.transform.position) <= DistanceToLaunch ))
            {
                gameObject.SetActive(true);
                shootPoint.PlaySpellCast();

                ShootPoint = shootPoint;
                transform.position = ShootPoint.GetPosition();

                if (DisablingWork != null)
                    StopCoroutine(DisablingWork);

                DisablingWork = StartCoroutine(Disabling());
                
                StartCoroutine(Rotating(shootPoint.GetRotationCenter() ,_radius));
            }
        }
    }
}
