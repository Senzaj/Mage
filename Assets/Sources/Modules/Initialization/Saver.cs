using System;
using Agava.YandexGames;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Training.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;
using Sources.Modules.Wave.Scripts;
using Sources.Modules.Workshop.Scripts;
using UnityEngine;

namespace Sources.Modules.Initialization
{
    public class Saver
    {
        private static Saver s_instance;
        private WaveSaver _wave = new();
        private WorkShopSaver _workShop = new();
        private WalletSaver _wallet = new();
        private TrainingSaver _training = new();
        private PlayerSaver _player = new();
        private VolumeSaver _volume = new();
        
        private AllDates _allDates;
        private event Action OnLoaded;

        public void Init(Action onLoaded)
        {
#if UNITY_EDITOR
            s_instance = this;
            Load(JsonUtility.ToJson("Test"));
            onLoaded.Invoke();
            return;
#endif
            
            if (s_instance == null)
            {
                TryLoad();
                
                s_instance = this;
                OnLoaded = onLoaded;
                _wave.RequestSave += OnRequestSave;
                _workShop.RequestSave += OnRequestSave;
                _wallet.RequestSave += OnRequestSave;
                _training.RequestSave += OnRequestSave;
                _player.RequestSave += OnRequestSave;
                _volume.RequestSave += OnRequestSave;
            }
            else
            {
                onLoaded?.Invoke();
            }
        }

        private void TryLoad() => PlayerAccount.GetCloudSaveData(onSuccessCallback: Load);

        private void Load(string json)
        {
            _allDates = JsonUtility.FromJson<AllDates>(json);
            _wave.Init(_allDates.Wave);
            _workShop.Init(_allDates.WorkShop);
            _wallet.Init(_allDates.Wallet);
            _training.Init(_allDates.Training);
            _player.Init(_allDates.Player);
            _volume.Init(_allDates.Volume);
            
            OnLoaded?.Invoke();
        }

        private void OnRequestSave<T>(T dataToSave)
        {
            switch (dataToSave)
            {
                case WaveData waveData:
                    _allDates.Wave = waveData;
                    Save();
                    break;
                case SpellSlotDates slotDates:
                    _allDates.WorkShop = slotDates;
                    Save();
                    break;
                case WalletData walletData:
                    _allDates.Wallet = walletData;
                    Save();
                    break;
                case TrainingData trainingData:
                    _allDates.Training = trainingData;
                    Save();
                    break;
                case PlayerData playerData:
                    _allDates.Player = playerData;
                    Save();
                    break;
                case VolumeData volumeData:
                    _allDates.Volume = volumeData;
                    Save();
                    break;
            }
        }

        private void Save()
        {
            #if !UNITY_WEBGL || UNITY_EDITOR
            return;
            #endif
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(_allDates));
        }
    }
}