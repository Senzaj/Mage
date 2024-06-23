using System;
using Sources.Modules.Training.Scripts;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Sources.Modules.Workshop.Scripts
{
    [RequireComponent(typeof(Button))]
    public class RewardCoin : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private TrainingView _trainingView;

        private Button _button;
        private const int MinCoins = 2;
        private const int EasyCoins = 5;
        private const int MediumCoins = 20;
        private const int MaxCoins = 40;
        private const int MaxCoinsRandomValue = 97;
        private const int MediumCoinsRandomValue = 85;
        private const int MaxRandomValue = 100;

        public event Action<int, int> RewardButtonClicked;
        public event Action<int> Rewarded;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void GetReward(int rewardCoins)
        {
            Rewarded?.Invoke(rewardCoins);
        }
        
        private void OnButtonClick()
        {
            if (_trainingView.isActiveAndEnabled)
                return;
            
            if (_yandex.IsInitialized)
                _yandex.ShowVideo(OnRewarded);
            else
                OnRewarded();
        }

        private void OnRewarded()
        {
            int randomValue = Random.Range(0, MaxRandomValue);
            int randomCoins;
            int chestAnimIndex;

            if (randomValue > MaxCoinsRandomValue)
            {
                randomCoins = Random.Range(MediumCoins, MaxCoins);
                chestAnimIndex = 2;
            }
            else if (randomValue > MediumCoinsRandomValue)
            {
                randomCoins = Random.Range(EasyCoins, MediumCoins);
                chestAnimIndex = 1;
            }
            else
            {
                randomCoins = Random.Range(MinCoins, EasyCoins);
                chestAnimIndex = 0;
            }
            
            RewardButtonClicked?.Invoke(randomCoins, chestAnimIndex);
        }
    }
}
