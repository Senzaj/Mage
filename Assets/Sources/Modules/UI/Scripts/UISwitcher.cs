using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.UI.Scripts
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] private List<Panel> _panels;

        private void Awake()
        {
            foreach (Panel panel in _panels)
            {
                panel.Enabled += OnPanelEnabled;
                panel.Disabled += OnPanelDisabled;

                if (panel.IsEnabled)
                    panel.TurnOnWithoutInvoke();
                else
                    panel.TurnOffWithoutInvoke();
            }
        }

        private void OnDisable()
        {
            foreach (Panel panel in _panels)
            {
                panel.Enabled -= OnPanelEnabled;
                panel.Disabled -= OnPanelDisabled;
            }
        }

        private void OnPanelEnabled(Panel activatedPanel)
        {
            if (activatedPanel.IsInGamePanel == false)
            {
                foreach (Panel panel in _panels)
                {
                    if (panel.IsEnabled && panel != activatedPanel)
                        panel.TurnOffWithoutInvoke();
                }

            }
        }

        private void OnPanelDisabled(Panel deactivatedPanel)
        {
            if (deactivatedPanel.IsInGamePanel == false)
            {
                foreach (Panel panel in _panels)
                {
                    if (panel.IsInGamePanel)
                    {
                        panel.TurnOn();
                        break;
                    }
                }
            }
        }
    }
}
