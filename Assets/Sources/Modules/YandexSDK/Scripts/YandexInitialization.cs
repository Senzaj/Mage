using System;
using System.Collections;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexInitialization : MonoBehaviour
    {
        public event Action Initialized;

        private IEnumerator Start()
        {
            Initialized?.Invoke();
            yield break;
        }
    }
}
