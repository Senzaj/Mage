using UnityEngine.EventSystems;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class WebEventSystem : EventSystem
    {
        protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
    }
}
