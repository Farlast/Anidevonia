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
    }
}
