using System;
using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexInitialization : MonoBehaviour
    {
        public event Action Initialized;
        
        private void Awake()
        {
#if UNITY_EDITOR
            return;
#endif
            
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            #if !UNITY_WEBGL || UNITY_EDITOR
            Initialized?.Invoke();
            yield break;
            #endif

            yield return YandexGamesSdk.Initialize(OnInitialized);
        }
        
        
        private void OnInitialized()
        {
            Initialized?.Invoke();
        }

        private void OnDestroy() => WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

        private void OnInBackgroundChange(bool inBackground)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }
    }
}
