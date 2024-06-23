using System;

namespace Sources.Modules.Player.Scripts.MVP
{
    internal class PlayerModel
    {
        private const float MaxHealthIncreaseValue = 10f;
        private const float DamageScalerIncreaseValue = 0.1f;
        private const float SpeedIncreaseValue = 0.1f;
        private readonly PlayerData _playerData;

        public bool CanRewarded;

        public event Action<float, float> MaxHealthChanged;
        public event Action<float, float> DamageScalerChanged;
        public event Action<float, float> SpeedChanged;
        public event Action<bool> CanRewardChanged;

        public float MaxHealth { get; private set; }
        public float DamageScaler { get; private set; }
        public float Speed { get; private set; }

        public PlayerModel(float maxHealth, float speed, float damageScaler, bool canRewarded)
        {
            MaxHealth = maxHealth;
            Speed = speed;
            DamageScaler = damageScaler;
            _playerData = new PlayerData();
            CanRewarded = canRewarded;

            SaveAll();
            CanRewardChanged?.Invoke(CanRewarded);
        }

        public void SetNewProperties(float maxHealth, float damageScaler, float speed)
        {
            MaxHealth = maxHealth;
            DamageScaler = damageScaler;
            Speed = speed;
            CanRewarded = true;
            _playerData.CanReward = CanRewarded;
            CanRewardChanged?.Invoke(CanRewarded);
            
            SaveAll();
            
            PlayerSaver.Instance.SaveData(_playerData);
        }

        public void InvokeAll()
        {
            MaxHealthChanged?.Invoke(MaxHealth, MaxHealthIncreaseValue);
            DamageScalerChanged?.Invoke(DamageScaler, DamageScalerIncreaseValue);
            SpeedChanged?.Invoke(Speed, SpeedIncreaseValue);
            CanRewardChanged?.Invoke(CanRewarded);
        }

        public void AddMaxHealth()
        {
            MaxHealth += MaxHealthIncreaseValue;
            MaxHealthChanged?.Invoke(MaxHealth, MaxHealthIncreaseValue);
            
            _playerData.MaxHealth = MaxHealth;
            PlayerSaver.Instance.SaveData(_playerData);
        }

        public void AddDamageScaler()
        {
            DamageScaler += DamageScalerIncreaseValue;
            DamageScalerChanged?.Invoke(DamageScaler, DamageScalerIncreaseValue);
            
            _playerData.DamageScaler = DamageScaler;
            PlayerSaver.Instance.SaveData(_playerData);
        }
        
        public void TryAddSpeed()
        {
            Speed += SpeedIncreaseValue;
            SpeedChanged?.Invoke(Speed, SpeedIncreaseValue);
            _playerData.Speed = Speed;
            
            PlayerSaver.Instance.SaveData(_playerData);
        }

        public void RewardedViewed()
        {
            CanRewarded = false;
            _playerData.CanReward = CanRewarded;
            
            CanRewardChanged?.Invoke(CanRewarded);
            PlayerSaver.Instance.SaveData(_playerData);
        }

        private void SaveAll()
        {
            _playerData.Speed = Speed;
            _playerData.DamageScaler = DamageScaler;
            _playerData.MaxHealth = MaxHealth;
            _playerData.CanReward = CanRewarded;
        }
    }
}