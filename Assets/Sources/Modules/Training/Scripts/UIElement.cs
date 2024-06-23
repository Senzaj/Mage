using UnityEngine;

namespace Sources.Modules.Training.Scripts
{
    public class UIElement : MonoBehaviour
    {
        public void Enable()
        {
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}