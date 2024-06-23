using System.Collections.Generic;
using Sources.Modules.CoinFactory.Scripts;
using Sources.Modules.Player.Scripts.MVP;
using Sources.Modules.Workshop.Scripts;
using Sources.Modules.Workshop.Scripts.UI;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    public class WalletPresenter
    {
        private readonly WalletModel _model;
        private readonly WalletView _view;
        private readonly List<Coin> _coins;
        private readonly CoinSpawner _coinSpawner;
        private readonly PlayerView _playerView;
        private readonly SpellsShop _spellsShop;
        private readonly RewardCoin _rewardCoin;
        
        private const int IncreaseCoinMultiplierPanel = 100;

        public WalletPresenter(WalletModel model, WalletView view, CoinSpawner coinSpawner, PlayerView playerView,
            SpellsShop spellsShop, RewardCoin rewardCoin)
        {
            _model = model;
            _view = view;
            _coinSpawner = coinSpawner;
            _playerView = playerView;
            _spellsShop = spellsShop;
            _coins = new List<Coin>();
            _rewardCoin = rewardCoin;
            _view.CoinMultiplierPanel.SetAddPrice(IncreaseCoinMultiplierPanel);
        }

        public void Enable()
        {
            PlayerViewEnable();
            _spellsShop.SlotBuyButtonPressed += OnSpellSlotButtonPressed;
            _view.CoinIncreasedButtonPressed += OnCoinIncreaseButtonPressed;
            _model.IncreaseChanged += OnIncreaseChanged;
            _rewardCoin.Rewarded += OnRewardedCoin;

            if (_coins.Count > 0)
                foreach (var coin in _coins)
                    coin.Taken += OnCoinTaken;

            _coinSpawner.CoinSpawned += OnCoinSpawned;
            _model.CoinsChanged += OnCoinsChanged;
            
            _model.InvokeAll();
        }

        public void Disable()
        {
            PlayerViewDisable();
            _spellsShop.SlotBuyButtonPressed -= OnSpellSlotButtonPressed;
            _view.CoinIncreasedButtonPressed -= OnCoinIncreaseButtonPressed;
            _model.IncreaseChanged -= OnIncreaseChanged;

            foreach (Coin coin in _coins)
                coin.Taken -= OnCoinTaken;
            
            _coinSpawner.CoinSpawned -= OnCoinSpawned;
            _model.CoinsChanged -= OnCoinsChanged;
        }

        public void Restart()
        {
            if (_coins.Count > 0) 
                foreach (Coin coin in _coins) 
                    coin.Taken -= OnCoinTaken;
        }

        private void OnMaxHealthIncreasingButtonPressed(int price)
        {
            if (_model.TryBuy(price))
                _playerView.AddMaxHealth();
        }
        
        private void OnDamageScalerIncreasingButtonPressed(int price)
        {
            if (_model.TryBuy(price))
                _playerView.AddDamageScaler();
        }
        
        private void OnSpeedIncreasingButtonPressed(int price)
        {
            if (_model.TryBuy(price))
                _playerView.AddSpeed();
        }

        private void OnCoinIncreaseButtonPressed(int price)
        {
            _model.TryBuyIncrease(price);
        }

        private void OnIncreaseChanged(int currentIncrease, int increase)
        {
            _view.ChangeCoinIncreaseText(currentIncrease, increase);
            _view.CoinMultiplierPanel.AddPrice();
        }

        private void OnSpellSlotButtonPressed(int price, SpellSlot slot)
        {
            if (_model.TryBuy(price))
                _spellsShop.BuySpell(slot);
        }
        
        private void OnCoinSpawned(Coin coin)
        {
            _coins.Add(coin);
            coin.Taken += OnCoinTaken;
        }
        
        private void OnCoinTaken(Coin coin)
        {
            _model.AddCoin();
            coin.Taken -= OnCoinTaken;
        }

        private void OnCoinsChanged(int coins)
        {
            _view.ChangeCoinText(coins); 
        }
        
        private void OnRewardedCoin(int coins)
        {
            _model.AddCoin(coins);
        }

        private void PlayerViewEnable()
        {
            _playerView.MaxHealthIncreasingButtonPressed += OnMaxHealthIncreasingButtonPressed;
            _playerView.DamageScalerIncreasingButtonPressed += OnDamageScalerIncreasingButtonPressed;
            _playerView.SpeedIncreasingButtonPressed += OnSpeedIncreasingButtonPressed;
        }
        
        private void PlayerViewDisable()
        {
            _playerView.MaxHealthIncreasingButtonPressed -= OnMaxHealthIncreasingButtonPressed;
            _playerView.DamageScalerIncreasingButtonPressed -= OnDamageScalerIncreasingButtonPressed;
            _playerView.SpeedIncreasingButtonPressed -= OnSpeedIncreasingButtonPressed;
        }
    }
}
