using UnityEngine;

namespace Script.Core
{
    public class ChainReactionProjectile : Projectile
    {
        [Header("Chain effect event")]
        [SerializeField] TransformEventChannel actionEvent;

        private bool hasActive;
       
        public override void SetUp(Vector2 origin, Vector2 shootDir, float lifeTime,ProjectilePool pool)
        {
            hasActive = false;
            base.SetUp(origin, shootDir, lifeTime, pool);
            TimerSystem.Create(() => { OnChainReaction(); }, lifeTime);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnChainReaction();
        }
        public void OnChainReaction()
        {
            if (hasActive) return;
            hasActive = true;
            rb.velocity = Vector2.zero;
            Releese();
            GetChainReaction();
        }
        void Releese()
        {
            pool.Release(this);
        }
        void GetChainReaction()
        {
            actionEvent?.RiseEvent(transform);
        }
    }
}
