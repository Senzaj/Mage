using Sources.Modules.UI.Scripts.InGame.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Wave.Scripts.UI
{
    internal class WaveIndicatorBar : Bar
    {
        [SerializeField] private WaveGenerator _waveGenerator;

        protected override void Awake()
        {
            Slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _waveGenerator.WaveStarted += UpdateMaxValue;
            _waveGenerator.UnitDied += DecreaseValueByOne;
            _waveGenerator.WaveEnded += SetMinValue;
        }

        private void OnDisable()
        {
            _waveGenerator.WaveStarted -= UpdateMaxValue;
            _waveGenerator.UnitDied -= DecreaseValueByOne;
            _waveGenerator.WaveEnded -= SetMinValue;
        }

        private void UpdateMaxValue(int value)
        {
            Slider.maxValue = value;
            ChangeValue(value);
        }

        private void DecreaseValueByOne()
        {
            float currentBarValue = Slider.value - 1;
            ChangeValue(currentBarValue);
        }

        private void SetMinValue()
        {
            ChangeValue(0);
        }
    }
}
