using UnityEngine;
using Script.Core;
using System.Collections;

namespace Script.Enemy
{
   
    public class Enemy : MonoBehaviour,IDamageable
    {
        [Header("Stats")]
        [SerializeField] internal EnemyData breed;
        [SerializeField] internal float currentHealth;
        [Header("Visual")]
        [SerializeField] private Transform spriteParent;
        [SerializeField] private Direction CurrentDirection;
        // Detector
        [SerializeField] internal Detector detector;

        private Direction oldDirection;

        private Material currentMaterial;
        private bool isFlashing;
        private float flashValue;
        private float maxFlashValue;
        internal Timer timer;

        private void Awake()
        {
            timer = GetComponent<Timer>();
            detector = GetComponent<Detector>();
        }
        void Start()
        {
            currentHealth = breed.Health.MaxHP;

            oldDirection = CurrentDirection;
            FlipSprite(CurrentDirection);

            maxFlashValue = 2;
            flashValue = 0;
            currentMaterial = new Material(breed.Material.FlashMat);
            currentMaterial.SetFloat("FlashValue", flashValue);
            Helpers.SetUpMaterial(currentMaterial, spriteParent);
        }

        private IEnumerator Flashing()
        {
            flashValue = maxFlashValue;
            isFlashing = true;
            while (flashValue > 0)
            {
                yield return null;
                flashValue -= 4.5f * Time.deltaTime;
                currentMaterial.SetFloat("FlashValue", flashValue);
            }
            isFlashing = false;
        }
        public void TakeDamage(DamageInfo info)
        {
            currentHealth -= info.Damage;
            currentHealth = currentHealth < 0 ? 0 : currentHealth;
            EffectPool.GetAndAutoReturnToPool(transform,0.3f);
            if (currentHealth <= 0) {
                Dead();
                return;
            }

            if (!isFlashing) StartCoroutine(Flashing());
            timer.SetTime(() =>{ print("delay finish"); },0.5f);

        }
       
        public void FlipSprite(Direction direction)
        {
            if (oldDirection != direction)
            {
                transform.eulerAngles = direction == Direction.Left ? Vector3.zero : new Vector3(0, 180, 0);
                oldDirection = direction;
            }
        }
        private void Dead()
        {
            Destroy(gameObject);
        }

    }
}
