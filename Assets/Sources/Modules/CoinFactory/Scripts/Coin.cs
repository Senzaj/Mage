using System;
using Sources.Modules.Player.Scripts;
using UnityEngine;

namespace Sources.Modules.CoinFactory.Scripts
{
    public class Coin : MonoBehaviour
    {
        public event Action<Coin> Taken;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Mage _))
            {
                Taken?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
