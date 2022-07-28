using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Core
{
    public class PlayerProjectilePool : PoolerBase<Projectile>
    {
        [SerializeField] private Projectile projectile;
        private void Start()
        {
            InitPool(projectile);
            var projecttile = Get();
            Release(projecttile);
        }
        private Projectile GetEffect(Transform target)
        {
            Projectile obj = Get();
            obj.transform.SetParent(transform);
            obj.transform.position = target.position;
            return obj;
        }
       
        public Projectile GetAndAutoReturnToPool(Transform target,Vector2 diraction, float time)
        {
            var returnEffect = GetEffect(target);
            returnEffect.SetUp(target.position,diraction,0.3f);
            TimerSystem.Create(() => { Release(returnEffect); }, time);
            return returnEffect;
        }
    }
}
