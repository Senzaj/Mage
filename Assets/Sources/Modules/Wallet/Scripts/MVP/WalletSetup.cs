using Sources.Modules.CoinFactory.Scripts;
using Sources.Modules.Player.Scripts.MVP;
using Sources.Modules.Player.Scripts.UI;
using Sources.Modules.Workshop.Scripts;
using Sources.Modules.Workshop.Scripts.UI;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    [RequireComponent(typeof(WalletView))]
    public class WalletSetup : MonoBehaviour
    {
        [SerializeField] private CoinSpawner _spawner;
        [SerializeField] private RewardCoin _rewardCoin;
        [SerializeField] private LosePanel _losePanel;
        
        private const int BaseIncrease = 0;
        
        private WalletView _view;
        private WalletPresenter _presenter;
        private WalletModel _model;
        
        private SpellsShop _spellsShop;

        public void Init(PlayerView playerView, SpellsShop spellsShop)
        {
            _view = GetComponent<WalletView>();
            
            _model = new (BaseIncrease);
            _presenter = new (_model, _view, _spawner, playerView, spellsShop, _rewardCoin);
        }

        private void OnEnable()
        {
            _presenter.Enable();
            _losePanel.Restarted += OnRestarted;
        }

        private void OnDisable()
        {
            _losePanel.Restarted -= OnRestarted;
            _presenter.Disable();
        }
        
        private void OnRestarted()
        {
            _model.Restart();
            _presenter.Restart();
        }
    }
}
