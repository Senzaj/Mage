using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts.LeaderBoard
{
    [RequireComponent(typeof(Panel))]
    public class Authorization : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private Button _acceptButton;

        private void OnEnable()
        {
            _acceptButton.onClick.AddListener(OnAcceptButtonClick);
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveListener(OnAcceptButtonClick);
        }

        private void OnAcceptButtonClick()
        {
            _yandex.OnAuthorizeButtonClick();
            GetComponent<Panel>().TurnOff();
        }
    }
}
