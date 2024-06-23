using System;
using Agava.YandexGames;
using Sources.Modules.YandexSDK.LanguageEnum;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexSdk : MonoBehaviour
    {
        [SerializeField] private bool _isInitialized;
        
        public Action AdOpened;
        public Action<bool> AdClosed;
        
        private string _currentLanguage;

        public bool IsInitialized => _isInitialized;

        private void Start()
        {
            if (_isInitialized)
            {
                SetLanguage();
                ShowStickyAd();
            }
        }

        private void SetLanguage()
        {
            _currentLanguage = YandexGamesSdk.Environment.i18n.lang;

            switch (_currentLanguage)
            {
                case "ru":
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageList.Russian);
                    break;

                case "uk":
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageList.Russian);
                    break;

                case "be":
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageList.Russian);
                    break;

                case "uz":
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageList.Russian);
                    break;

                case "kk":
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageList.Russian);
                    break;

                case "tr":
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageList.Turkish);
                    break;

                default:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageList.English);
                    break;
            }
        }

        public void ShowInterstitial()
        {
            InterstitialAd.Show(OnAdOpened, OnAdClosed);
        }

        public void ShowVideo(Action onRewarded)
        {
            VideoAd.Show(OnAdOpened, onRewarded, OnAdClosed);
        }

        public void OnAuthorizeButtonClick()
        {
            PlayerAccount.Authorize();
        }

        public void RequestPersonalProfileDataPermission()
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
        }

        private void OnAdOpened()
        {
            AdOpened.Invoke();
        }

        private void ShowStickyAd()
        {
            StickyAd.Show();
        }

        private void OnAdClosed(bool showed)
        {
            AdClosed.Invoke(showed);
        }

        private void OnAdClosed()
        {
            AdClosed.Invoke(true);
        }
    }
}
