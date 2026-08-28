using System;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexSdk : MonoBehaviour
    {
        public bool IsInitialized => false;

        public void ShowInterstitial()
        {
        }

        public void ShowVideo(Action onRewarded)
        {
            onRewarded?.Invoke();
        }

        public void OnAuthorizeButtonClick()
        {
        }

        public void RequestPersonalProfileDataPermission()
        {
        }
    }
}
