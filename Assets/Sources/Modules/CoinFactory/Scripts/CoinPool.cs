using System.Collections.Generic;
using Sources.Modules.Pools.Scripts;
using UnityEngine;

namespace Sources.Modules.CoinFactory.Scripts
{
    internal class CoinPool : Pool<Coin>
    {
        private List<Coin> _coins;

        private void Awake()
        {
            _coins = new List<Coin>();
            
            for (int i = 0; i < StartCapacity; i++)
            {
                Coin spawned = InitCoin();
                spawned.gameObject.SetActive(false);
                _coins.Add(spawned);
            }
        }

        public Coin GetCoin()
        {
            Coin inactiveCoin = null;

            foreach (Coin coin in _coins)
            {
                if (coin.isActiveAndEnabled == false)
                {
                    inactiveCoin = coin;
                    break;
                }
            }

            if (inactiveCoin == null)
            {
                Coin spawned = InitCoin();
                _coins.Add(spawned);
                inactiveCoin = spawned;
            }

            return inactiveCoin;
        }

        public List<Coin> GetCoins() => _coins.GetRange(0, _coins.Count);

        private Coin InitCoin()
        {
            Coin spawned = Instantiate(Prefabs[Random.Range(0, Prefabs.Count)], transform.position, Quaternion.identity,
                transform);
            
            return spawned;
        }
    }
}
