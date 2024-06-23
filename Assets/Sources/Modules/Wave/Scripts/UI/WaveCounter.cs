using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Wave.Scripts.UI
{
    public class WaveCounter : MonoBehaviour
    {
        [SerializeField] private WaveGenerator _waveGenerator;
        [SerializeField] private TMP_Text _text;

        private const string WaveString = "WaveCounter";

        private void OnEnable()
        {
            _waveGenerator.WaveCountChanged += ChangeText;
        }

        private void OnDisable()
        {
            _waveGenerator.WaveCountChanged -= ChangeText;
        }

        private void ChangeText(int waveCount)
        {
            _text.text = ($"{LeanLocalization.GetTranslationText(WaveString)}: {waveCount}");
        }
    }
}
