using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Base.Spells
{
    internal class PreDirectedByMageDirection : Projectile
    {
        public override void TryLaunch(ShootPoint shootPoint, Vector3 position)
        {
            if ((Vector2.Distance(position, shootPoint.transform.position) <= DistanceToLaunch ))
            {
                gameObject.SetActive(true);
                shootPoint.PlaySpellCast();
                transform.up = (shootPoint.GetMageDirection().position - shootPoint.transform.position);

                ShootPoint = shootPoint;
                transform.position = ShootPoint.GetPosition();

                if (DisablingWork != null)
                    StopCoroutine(DisablingWork);

                DisablingWork = StartCoroutine(Disabling());
                
                StartCoroutine(ChangingPosition(shootPoint.GetMageDirection().position));
            }
        }
    }
}
