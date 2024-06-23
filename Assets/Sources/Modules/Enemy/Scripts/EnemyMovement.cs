using System.Collections;
using UnityEngine;
using Pathfinding;
using Sources.Modules.Common;

namespace Sources.Modules.Enemy
{
    [RequireComponent(typeof(Flipper))]
    internal class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private AIPath _aiPath;
        
        private const float FlippedDelay = 0.1f;

        private Flipper _flipper;
        private Coroutine _flippedWork;

        private void Awake() => _flipper = GetComponent<Flipper>();

        private void OnEnable()
        {
            _flippedWork = StartCoroutine(Flipped());
        }

        private void OnDisable()
        {
            StopCoroutine(_flippedWork);
        }

        private IEnumerator Flipped()
        {
            WaitForSeconds waitForSeconds = new(FlippedDelay);
            
            while (isActiveAndEnabled)
            {
                _flipper.TryFlip(_aiPath.desiredVelocity.x);

                yield return waitForSeconds;
            }
        }
    }
}
