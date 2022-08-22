using UnityEngine;
using Script.Core;

namespace Script.Enemy
{
    public interface IEnemy
    {
        protected enum BehaviorType
        {
            Dummy,
            Wander,
            Guard,
            GuardAndFollowTraget,
            WanderAndFollowTraget
        }

        void Attack();
    }
    public class GroundEnemy : MonoBehaviour, IEnemy, IDamageable
    {
        public void Attack()
        {
            
        }

        public void TakeDamage(DamageInfo info)
        {
            
        }
    }
}
