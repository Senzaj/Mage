using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts.InGame.Common
{
    [RequireComponent(typeof(Slider))]
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] private float _durationOfChange;
        
        protected Slider Slider;
        
        protected virtual void Awake() => Slider = GetComponent<Slider>();
        
        protected void ChangeValue(float value)
        {
            Slider.DOValue(value, _durationOfChange);
        }
    }
}
