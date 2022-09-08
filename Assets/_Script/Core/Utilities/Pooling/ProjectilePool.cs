using UnityEngine;

namespace Script.Core
{
    public class ProjectilePool : PoolerBase<Projectile>
    {
        [SerializeField] private Projectile projectile;
        private void Start()
        {
            InitPool(projectile);
        }
        public Projectile GetAndSetPosition(Transform target)
        {
            Projectile obj = Get();
            obj.transform.SetParent(transform);
            obj.transform.position = target.position;
            return obj;
        }
      
        public Projectile GetAndAutoReturnToPool(Transform target,Vector2 diraction, float time)
        {
            var returnEffect = GetAndSetPosition(target);
            returnEffect.SetUp(target.position,diraction,0.3f,this);
            TimerSystem.Create(() => { Release(returnEffect); }, time);
            return returnEffect;
        }
    }
}
