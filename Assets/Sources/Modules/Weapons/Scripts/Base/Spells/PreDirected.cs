using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Base.Spells
{
    internal class PreDirected : Projectile
    {
        [SerializeField] private Vector2 _targetPosition;
        
        public override void TryLaunch(ShootPoint shootPoint, Vector3 position)
        {
            if ((Vector2.Distance(position, shootPoint.transform.position) <= DistanceToLaunch ))
            {
                gameObject.SetActive(true);
                shootPoint.PlaySpellCast();
                transform.up = _targetPosition.normalized;

                ShootPoint = shootPoint;
                transform.position = ShootPoint.GetPosition();

                if (DisablingWork != null)
                    StopCoroutine(DisablingWork);

                DisablingWork = StartCoroutine(Disabling());
                
                StartCoroutine(ChangingPosition(_targetPosition));
            }
        }
    }
}
