using System;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    public class WalletModel
    {
        private const int IncreaseCoin = 1;
        private const int MinAddCoin = 1;
        
        private int _coins;
        private int _addCoins;
        private readonly WalletData _walletData;
        
        public event Action<int> CoinsChanged; 
        public event Action<int, int> IncreaseChanged;

        public WalletModel(int addCoins)
        {
            _walletData = WalletSaver.Instance.GetData() ?? new WalletData()
            {
                Coins = 0
            };
            _coins = _walletData.Coins;
            _addCoins = Mathf.Clamp(addCoins, MinAddCoin, Int32.MaxValue);
        }

        public void InvokeAll()
        {
            CoinsChanged?.Invoke(_coins);
            IncreaseChanged?.Invoke(_addCoins, IncreaseCoin);
        }

        public void Restart()
        {
            _coins = 0;
            _addCoins = MinAddCoin;
            Save();
            CoinsChanged?.Invoke(_coins);
        }
        
        public void AddCoin(int value = -1)
        {
            if (value > 0)
                _coins += value;
            else
                _coins += _addCoins;

            Save();
            CoinsChanged?.Invoke(_coins);
        }

        public bool TryBuy(int price)
        {
            if (_coins - price < 0)
                return false;

            _coins -= price;
            Save();
            CoinsChanged?.Invoke(_coins);
            return true;
        }

        public void TryBuyIncrease(int price)
        {
            if (_coins - price < 0)
                return;

            _coins -= price;
            _addCoins += IncreaseCoin;
            Save();
            CoinsChanged?.Invoke(_coins);
            IncreaseChanged?.Invoke(_addCoins, IncreaseCoin);
        }

        private void Save()
        {
            _walletData.Coins = _coins;
            WalletSaver.Instance.SaveData(_walletData);
        }
    }
}
