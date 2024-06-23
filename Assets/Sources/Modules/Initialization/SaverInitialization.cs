using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Modules.Initialization
{
    [RequireComponent(typeof(YandexInitialization))]
    public class SaverInitialization : MonoBehaviour
    {
        private const string GameScene = nameof(GameScene);
        private YandexInitialization _yandexInitialization;

        private void Awake()
        {
            _yandexInitialization = GetComponent<YandexInitialization>();
        }

        private void OnEnable()
        {
            _yandexInitialization.Initialized += Init;
        }

        private void OnDisable()
        {
            _yandexInitialization.Initialized -= Init;
        }

        private void Init()
        {
            Saver saver = new();
            saver.Init(Loaded);
        }

        private void Loaded()
        {
            SceneManager.LoadScene(GameScene);
        }
    }
}