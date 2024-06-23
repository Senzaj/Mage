using Agava.WebUtility;
using Sources.Modules.Common;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class AudioListenerSwitch : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private StoppingTime _time;

        private void OnEnable()
        {
            _yandex.AdOpened += OnInterstitialOpened;
            _yandex.AdClosed += OnInterstitialClosed;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            _yandex.AdOpened -= OnInterstitialOpened;
            _yandex.AdClosed -= OnInterstitialClosed;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        private void OnInterstitialOpened()
        {
            _time.AdPause();
            AudioListener.pause = true;
            AudioListener.volume = 0;
        }

        private void OnInterstitialClosed(bool wasShown)
        {
            _time.AdPlay();
            AudioListener.pause = false;
            AudioListener.volume = 1;
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            if (inBackground)
                _time.BackgroundPause();
            else
                _time.BackgroundPlay();

            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }
    }
}
