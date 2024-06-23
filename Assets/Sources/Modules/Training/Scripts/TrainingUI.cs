using UnityEngine;

namespace Sources.Modules.Training.Scripts
{
    public class TrainingUI : MonoBehaviour
    {
        [SerializeField] private CloseButton _closeButton;

        public CloseButton CloseButton => _closeButton;
    }
}