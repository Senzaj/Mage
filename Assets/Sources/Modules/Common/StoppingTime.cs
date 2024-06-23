using UnityEngine;

namespace Sources.Modules.Common
{
    public class StoppingTime : MonoBehaviour
    {
        private bool _isPausedByPanel = false;
        private bool _isPausedByAd = false;
        private bool _isPausedByBackground = false;

        public void PanelPause()
        {
            _isPausedByPanel = true;
            StopTime();
        }

        public void PanelPlay()
        {
            _isPausedByPanel = false;
            TryPlayTime();
        }
        
        public void AdPause()
        {
            _isPausedByAd = true;
            StopTime();
        }

        public void AdPlay()
        {
            _isPausedByAd = false;
            TryPlayTime();
        }
        
        public void BackgroundPause()
        {
            _isPausedByBackground = true;
            StopTime();
        }

        public void BackgroundPlay()
        {
            _isPausedByBackground = false;
            TryPlayTime();
        }

        private void StopTime()
        {
            Time.timeScale = 0;
        }

        private void TryPlayTime()
        {
            if (_isPausedByPanel == false && _isPausedByAd == false && _isPausedByBackground == false)
                Time.timeScale = 1;
        }
    }
}
