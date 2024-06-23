using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Training.Scripts
{
    public class TrainingInput : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private TrainingView _trainingView;

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(OnButtonClick);
            _trainingView.RequestNextButtonDisable += OnRequestNextButtonDisable;
            _trainingView.RequestNextButtonEnable += OnRequestNextButtonEnable;
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(OnButtonClick);
            _trainingView.RequestNextButtonDisable -= OnRequestNextButtonDisable;
            _trainingView.RequestNextButtonEnable -= OnRequestNextButtonEnable;
        }
        
        private void OnRequestNextButtonDisable()
        {
            _nextButton.gameObject.SetActive(false);
        }
        
        private void OnRequestNextButtonEnable()
        {
            _nextButton.gameObject.SetActive(true);
        }

        private void OnButtonClick()
        {
            _trainingView.NextSlide();
        }
    }
}