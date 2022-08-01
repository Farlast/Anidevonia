using UnityEngine;
using UnityEngine.UI;

namespace Script.Core.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        private Slider slider;
        [SerializeField] private HealthEvent healthEvent;

        void Awake()
        {
            slider = GetComponent<Slider>();
        }
        private void Start()
        {
            healthEvent.Ondamage += OnHpChange;
        }

        private void OnHpChange(float hp)
        {
            slider.value = hp;
        }
    }
}
