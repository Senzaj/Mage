using Sources.Modules.UI.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Workshop.Scripts.UI
{
    [RequireComponent(typeof(Panel))]
    public class RewardCoinUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openChestButton;
        [SerializeField] private RewardCoin _rewardCoinButton;
        [SerializeField] private CanvasGroup _coinsPanel;
        [SerializeField] private TMP_Text _coinsCount;
        [SerializeField] private ParticleSystem _coinsBurst;
        [SerializeField] private Animator _chestAnimator;
        [SerializeField] private Chest _chest;
        
        private int _rewardCoins = 0;
        private int _chestAnimIndex;

        private void OnEnable()
        {
            GetComponent<Panel>().TurnOffWithoutInvoke();
            _openChestButton.onClick.AddListener(OpenChest);
            _rewardCoinButton.RewardButtonClicked += OnRewardButtonClicked;
            _chest.Opened += OnChestOpened;
        }

        private void OnDisable()
        {
            _openChestButton.onClick.RemoveListener(OpenChest);
            _rewardCoinButton.RewardButtonClicked -= OnRewardButtonClicked;
            _chest.Opened -= OnChestOpened;
        }

        private void OnRewardButtonClicked(int rewardCoins, int chestAnimIndex)
        {
            _rewardCoins = rewardCoins;
            _chestAnimIndex = chestAnimIndex;
            _closeButton.gameObject.SetActive(false);
            _openChestButton.gameObject.SetActive(true);
            _coinsPanel.alpha = 0;
            _chestAnimator.Play(ChestAnimator.States.Idle);
            GetComponent<Panel>().TurnOnWithoutInvoke();
        }

        private void OpenChest()
        {
            _openChestButton.gameObject.SetActive(false);
            
            switch (_chestAnimIndex)
            {
                case 0:
                    _chestAnimator.Play(ChestAnimator.States.Chest0);
                    break;

                case 1:
                    _chestAnimator.Play(ChestAnimator.States.Chest1);
                    break;

                case 2:
                    _chestAnimator.Play(ChestAnimator.States.Chest2);
                    break;
            }
        }
        
        private void OnChestOpened()
        {
            var emisson = _coinsBurst.emission;
            emisson.burstCount = _rewardCoins;
            _coinsBurst.Play();
            _coinsPanel.alpha = 1;
            _coinsCount.text = _rewardCoins.ToString();
            _closeButton.gameObject.SetActive(true);
            _rewardCoinButton.GetReward(_rewardCoins);
        }
    }
}
