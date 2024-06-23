using System.Collections.Generic;
using Agava.YandexGames;
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

                if (activatedPanel.IsLeaderboard)
                {
                    bool isAuthorized = PlayerAccount.IsAuthorized;

                    if (isAuthorized)
                    {
                        foreach (Panel panel in _panels)
                        {
                            if (panel.IsEnabled && panel != activatedPanel)
                                panel.TurnOffWithoutInvoke();
                        }
                    }
                    else
                    {
                        foreach (Panel panel in _panels)
                        {
                            if (panel.IsEnabled)
                                panel.TurnOffWithoutInvoke();
                            
                            if (panel.IsAuthorization)
                                panel.TurnOnWithoutInvoke();
                        }
                    }
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
