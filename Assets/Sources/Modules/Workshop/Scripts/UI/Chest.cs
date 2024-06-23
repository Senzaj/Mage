using System;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts.UI
{
    public class Chest : MonoBehaviour
    {
        public event Action Opened;

        private void OnOpened() => Opened?.Invoke();
    }
}
