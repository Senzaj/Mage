using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Base.Spells
{
    internal class Rotating : Projectile
    {
        [SerializeField] private float _radius;

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
