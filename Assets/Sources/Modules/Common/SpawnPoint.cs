using UnityEngine;

namespace Sources.Modules.Common
{
    public class SpawnPoint : MonoBehaviour
    {
        private bool _isReadyToSpawn = true;

        public bool IsReadyToSpawn => _isReadyToSpawn;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Obstacle>(out _))
                _isReadyToSpawn = false;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Obstacle>(out _))
                _isReadyToSpawn = true;
        }
    }
}
