using UnityEngine.Events;
using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/HealthEvent")]
    public class HealthEvent : ScriptableObject
    {
        public event UnityAction<float> Ondamage;
        public event UnityAction<float> OnHeal;

        public virtual float Damage(DamageInfo damage,float health, float maxHealth)
        {
            health -= damage.Damage;
            if (health < 0) health = 0;
            Ondamage?.Invoke(GetHealthNormalized(health, maxHealth));
            return health;
        }
        public virtual float Heal(float amount,float health,float maxHealth)
        {
            health += amount;
            if (health > maxHealth)  health = maxHealth;
            OnHeal?.Invoke(GetHealthNormalized(health, maxHealth));
            return health;
        }
        
        private float GetHealthNormalized(float health, float maxHealth)
        {   //   get % health
            return health / maxHealth;
        }
    }
}
