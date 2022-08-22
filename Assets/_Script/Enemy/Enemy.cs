using UnityEngine;
using Script.Core;
using System;

namespace Script.Enemy
{
   
    public class Enemy : MonoBehaviour,IDamageable
    {
        [Header("Stats")]
        [SerializeField] internal Stats Stats;
        [SerializeField] internal float hp;

        [SerializeField] private Transform spriteParent;
        [SerializeField] private GameObject attackBox;
        [SerializeField] private StateController stateController;

        [SerializeField] private Transform attackPoint;
        [SerializeField] private Vector2 attackRange;
        [SerializeField] private LayerMask attackLayer;

        private Material currentMaterial;
        private float flashValue;
        private float maxFlashValue;
        private float oldDirection;

        void Start()
        {
            hp = Stats.MaxHP;

            maxFlashValue = 2;
            flashValue = 0;

            SetUpMaterial(new Material(Stats.FlashMat));
        }
        private void SetUpMaterial(Material material)
        {
            currentMaterial = material;
            currentMaterial.SetFloat("FlashValue", flashValue);
            
            for (int i = 0; i < spriteParent.childCount; i++)
            {
                GameObject child = spriteParent.GetChild(i).gameObject;

                if (child.TryGetComponent(out SpriteRenderer sprite))
                {
                    sprite.material = currentMaterial;
                }
            }
        }
        void Update()
        {
            Flashing();
        }
        void Flashing()
        {
            if (flashValue <= 0) return;
            flashValue -= 4.5f * Time.deltaTime;
            currentMaterial.SetFloat("FlashValue", flashValue);
        }
        public void TakeDamage(DamageInfo info)
        {
            hp -= info.Damage;
            hp = hp < 0 ? 0 : hp;
            flashValue = maxFlashValue;
            EffectPool.GetAndAutoReturnToPool(transform,0.3f);
            if (hp <= 0) {
                Dead();
                return;
            }
            stateController.Takehit = true;
        }
       
        public void FlipSprite(float lastDiraction)
        {
            if(oldDirection != lastDiraction)
            {
                transform.eulerAngles = lastDiraction < 0 ? Vector3.zero : new Vector3(0, 180, 0);
                oldDirection = lastDiraction;
            }
        }

        private void Dead()
        {
            Destroy(gameObject);
        }

        public void MeleeAttack()
        {
            Collider2D[] TargetToDamage = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRange.x, attackRange.y), 0, attackLayer);

            if (TargetToDamage == null) return;

            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                IDamageable damageable = TargetToDamage[i].GetComponent<IDamageable>();

                if (damageable == null) { continue; }
                else
                {
                    DamageInfo info = new(Stats.AttackDamage, transform.position)
                    {
                        KnockBack = KnockbackType.Low
                    };
                    damageable.TakeDamage(info);
                }
            }
        }
        #region Gizmos
        void OnDrawGizmos()
        {
            if (attackPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(attackPoint.position, new Vector2(attackRange.x, attackRange.y));
            }
        }
        #endregion
    }
}
