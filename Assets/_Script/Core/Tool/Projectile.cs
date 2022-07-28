using System.Collections;
using UnityEngine;

namespace Script.Core
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackDamage;

        private float moveTime;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void SetUp(Vector2 origin,Vector2 shootDir, float lifeTime)
        {
            transform.position = origin;
            moveTime = lifeTime;
            StartCoroutine(Move(shootDir));
        }
        IEnumerator Move(Vector2 diraction)
        {
            float counter = 0;
            
            rb.velocity = new Vector2(diraction.x * moveSpeed, diraction.y * moveSpeed);
            
            while (counter < moveTime)
            {
                counter += Time.deltaTime;
                yield return null;
            }
            print("finish");
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Attack(collision);
        }
        public virtual void Attack(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable target))
            {
                DamageInfo info = new DamageInfo(attackDamage, transform.position);
                target.TakeDamage(info);
            }
            else
            {
                print("ground des");
                gameObject.SetActive(false);
                EffectPool.GetAndAutoReturnToPool(transform, 0.3f);
            }
        }
    }
}
