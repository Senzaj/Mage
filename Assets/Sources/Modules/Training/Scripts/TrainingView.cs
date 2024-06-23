using System;
using Sources.Modules.Training.Enums;
using UnityEngine;

namespace Sources.Modules.Training.Scripts
{
    public class TrainingView : MonoBehaviour
    {
        [SerializeField] private TrainingUI[] _trainingUis;
        [SerializeField] private UIElement[] _uiElementsToEnable;

        private int _currentSlideIndex = 1;
        private int _elementIndex = 0;

        public event Action RequestNextButtonEnable;
        public event Action RequestNextButtonDisable;
        public event Action RequestExitButtonEnable;
        public event Action RequestExitButtonDisable;
        public event Action RequestDisableInput;
        public event Action RequestEnableInput;

        private void Start()
        {
            if (TrainingSaver.Instance.GetData()?.IsTrained ?? false)
            {
                gameObject.SetActive(false);
            }
            else
            {
                foreach (var element in _uiElementsToEnable)
                    element.Disable();
            
                RequestDisableInput?.Invoke();
            }
        }

        private void OnEnable()
        {
            foreach (var trainingUI in _trainingUis)
            {
                trainingUI.CloseButton.RequestClose += OnRequestClose;
            }
        }

        public void NextSlide()
        {
            if (TrainingSaver.Instance.GetData()?.IsTrained ?? false)
                return;

            if (_currentSlideIndex >= _trainingUis.Length)
            {
                Disable();
                return;
            }

            if (_currentSlideIndex > 0)
            {
                switch (_currentSlideIndex)
                {
                    case (int) TrainingObjects.PlayerHealth:
                        EnableCurrentElement();
                        break;
                    case (int) TrainingObjects.PlayerWallet:
                        EnableCurrentElement();
                        break;
                    case (int) TrainingObjects.GoToShop:
                        RequestNextButtonDisable?.Invoke();
                        RequestEnableInput?.Invoke();
                        break;
                    case (int) TrainingObjects.Upgrades:
                        RequestExitButtonDisable?.Invoke();
                        break;
                    case (int) TrainingObjects.CloseShop:
                        RequestNextButtonDisable?.Invoke();
                        RequestExitButtonEnable?.Invoke();
                        break;
                    case (int) TrainingObjects.WaveIndicator:
                        EnableCurrentElement();
                        break;
                    case (int) TrainingObjects.WaveCount:
                        EnableCurrentElement();
                        break;
                    case (int) TrainingObjects.LeaderBoard:
                        EnableCurrentElement();
                        break;
                    case (int) TrainingObjects.Settings:
                        EnableCurrentElement();
                        break;
                    case (int) TrainingObjects.NextWave:
                        EnableCurrentElement();
                        break;
                }

                _trainingUis[_currentSlideIndex - 1].gameObject.SetActive(false);
            }
            
            _trainingUis[_currentSlideIndex].gameObject.SetActive(true);
            _currentSlideIndex++;
        }


        public void TryNextSlide()
        {
            if (TrainingSaver.Instance.GetData()?.IsTrained ?? false)
                return;
            
            if ((int)TrainingObjects.GoToShop == _currentSlideIndex - 1)
                NextSlide();
        }
        
        public void EnableButton()
        {
            RequestNextButtonEnable?.Invoke();
        }

        public void Disable()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                TrainingSaver.Instance.SaveData(new TrainingData
                {
                    IsTrained = true
                });
            }
        }

        private void OnRequestClose()
        {
            if (TrainingSaver.Instance.GetData()?.IsTrained ?? false)
                return;

            RequestEnableInput?.Invoke();
            RequestExitButtonEnable?.Invoke();

            foreach (var uiElement in _uiElementsToEnable)
                uiElement.Enable();

            Disable();
        }

        private void EnableCurrentElement()
        {
            _uiElementsToEnable[_elementIndex].Enable();
            _elementIndex++;
        }
    }
}
