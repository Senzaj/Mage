using System;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Training.Scripts;
using Sources.Modules.Wave.Scripts;
using Sources.Modules.Wave.Scripts.UI;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts
{
    internal class WorkshopTrigger : MonoBehaviour
    {
        [SerializeField] private WaveGenerator _waveGenerator;
        [SerializeField] private WaveStartWaveUI _waveStartWaveUI;

        private bool _isWave = false;
        private bool _isPlayerIn = false;
        
        public event Action PlayerEntered;
        public event Action PlayerCameOut;

        private void OnEnable()
        {
            _waveStartWaveUI.NextWaveButtonPressed += OnWaveStarted;
            _waveGenerator.WaveEnded += OnWaveEnded;
        }

        private void OnDisable()
        {
            _waveStartWaveUI.NextWaveButtonPressed -= OnWaveStarted;
            _waveGenerator.WaveEnded -= OnWaveEnded;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Mage>(out _))
            {
                _isPlayerIn = true;

                if (_isWave == false)
                    PlayerEntered?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Mage>(out _))
            {
                _isPlayerIn = false;
                
                if (_isWave == false)
                    PlayerCameOut?.Invoke();
            }
        }

        private void OnWaveStarted()
        {
            _isWave = true;
            PlayerCameOut?.Invoke();
        }

        private void OnWaveEnded()
        {
            _isWave = false;
            
            if (_isPlayerIn)
                PlayerEntered?.Invoke();
        }
    }
}
