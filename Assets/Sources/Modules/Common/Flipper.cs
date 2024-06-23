using UnityEngine;

namespace Sources.Modules.Common
{
    public class Flipper : MonoBehaviour
    {
        private const float MinVelocityToFlip = 0.1f;

        private Vector3 _flipScale;
        private Vector3 _baseScale;

        private bool IsFlipped => transform.localScale.x < 0;

        private void Awake()
        {
            _baseScale = transform.localScale;
            _flipScale = _baseScale;
            _flipScale.x *= -1;
        }

        public void TryFlip(float velocityX)
        {
            if (velocityX == 0)
                return;

            if (velocityX < MinVelocityToFlip && IsFlipped == false)
                transform.localScale = _flipScale;

            else if (velocityX > MinVelocityToFlip && IsFlipped)
                transform.localScale = _baseScale;
        }
    }
}