using Sources.Modules.Training.Scripts;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts.UI
{
    internal class WorkshopButton : MonoBehaviour
    {
        [SerializeField] private WorkshopTrigger _trigger;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TrainingView _trainingView;

        private void Awake()
        {
            TurnOff();
        }

        private void OnEnable()
        {
            _trigger.PlayerEntered += TurnOn;
            _trigger.PlayerCameOut += TurnOff;
        }

        private void OnDisable()
        {
            _trigger.PlayerEntered -= TurnOn;
            _trigger.PlayerCameOut -= TurnOff;
        }

        private void TurnOn()
        {
            _trainingView.TryNextSlide();
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void TurnOff()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
