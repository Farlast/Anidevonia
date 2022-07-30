using System.Collections;
using UnityEngine;

namespace Script.Core
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float attackDamage;
        [SerializeField] protected ProjectilePool pool;
        protected Rigidbody2D rb;
        protected float lifeTime;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public virtual void SetUp(Vector2 origin, Vector2 shootDir, float lifeTime, ProjectilePool pool)
        {
            this.pool = pool;
            transform.position = origin;
            this.lifeTime = lifeTime;
            Rotation(shootDir);
            StartCoroutine(Move(shootDir));
        }
        protected virtual void Rotation(Vector2 shootDir)
        {
            transform.eulerAngles = shootDir.x < 0 ? new Vector2(0, 180) : new Vector2(0, 0);
        }
        protected virtual IEnumerator Move(Vector2 diraction)
        {
            rb.velocity = Vector2.zero;
            yield return new WaitForFixedUpdate();
            rb.velocity = new Vector2(diraction.x * moveSpeed, diraction.y * moveSpeed);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Attack(collision);
        }
        protected virtual void Attack(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable target))
            {
                DamageInfo info = new DamageInfo(attackDamage, transform.position);
                target.TakeDamage(info);
            }
            else
            {
                OnHitGroundOrExpire();
            }
        }
        public void OnHitGroundOrExpire()
        {
            pool.Release(this);
            EffectPool.GetAndAutoReturnToPool(transform, 0.3f);
        }
    }
}
