using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Training.Scripts
{
    [RequireComponent(typeof(Button))]
    public class CloseButton : MonoBehaviour
    {
        private Button _button;

        public event Action RequestClose; 

        private void Awake() => _button = GetComponent<Button>();
        
        private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick() => RequestClose?.Invoke();

    }
}
