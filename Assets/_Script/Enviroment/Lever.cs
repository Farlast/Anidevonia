using UnityEngine;
using UnityEngine.Events;
using Script.Core;

namespace Script.Enemy
{
    public class Lever : MonoBehaviour,IDamageable
    {
        [SerializeField] UnityEvent Event;

        public void TakeDamage(DamageInfo damage)
        {
            Event?.Invoke();
        }
    }
}
