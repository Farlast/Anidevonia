using UnityEngine;
using Script.Core;
using System.Collections;

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
    public class EnemyBase : MonoBehaviour, IEnemy, IDamageable
    {
        
        [SerializeField] internal float CurrentHP;
        [SerializeField] internal Detector EyeView;
        [SerializeField] internal EnemyData Data;
        public EnemyAnimation EnemyAnimation;

        protected Rigidbody2D Rb;
        public Vector2 NewVector;
        protected float LastDiraction;
        private Direction oldDirection;

        [SerializeField] private Transform spriteParent;
        private bool IsFlashing;
        private Material currentMaterial;
        private float flashValue;
        private float maxFlashValue;

        public bool TakeHit;
        public int CurrentHitTake;
        public bool IsAttackReady = true;

        #region Setup
        private void Awake()
        {
            EnemyAnimation = GetComponent<EnemyAnimation>();
        }
        protected virtual void Start()
        {
            CurrentHitTake = 0;
            flashValue = 0;
            maxFlashValue = 2;
            IsFlashing = false;
            SetUpMaterial(new Material(Data.Material.FlashMat));
        }
        protected void SetUpMaterial(Material material)
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
        #endregion

        #region Reuseable
        public void AttackCoolDown()
        {
            IsAttackReady = false;
            TimerSystem.Create(() => { IsAttackReady = true; },Data.Combat.AttackSpeed);
        }
        public void SetVelocity(Vector2 velocity)
        {
            Rb.velocity = velocity;
        }
        public void ResetVelocity()
        {
            NewVector.Set(0, 0);
            Rb.velocity = NewVector;
        }
        public virtual void Attack()
        {
        }
        public virtual void TakeDamage(DamageInfo info)
        {
            CurrentHP -= info.Damage;
            flashValue = maxFlashValue;
            if (!IsFlashing) StartCoroutine(Flashing());
            if (CurrentHP <= 0)
            {
                Dead();
                return;
            }
            EffectPool.GetAndAutoReturnToPool(transform, 0.3f);
        }

        protected virtual void Dead()
        {
            Destroy(gameObject);
        }
        public virtual void Move()
        {

        }
        public void FlipSprite(Direction direction)
        {
            if (oldDirection != direction)
            {
                transform.eulerAngles =  direction == Direction.Left? Vector3.zero : new Vector3(0, 180, 0);
                oldDirection = direction;
            }
        }
        public Direction GetFaceingDiraction()
        {
            return LastDiraction <= 0 ? Direction.Left : Direction.Right;
        }
        public Direction GetFaceingDiraction(Vector2 diraction)
        {
            return diraction.x <= 0 ? Direction.Left : Direction.Right;
        }
        #endregion

        #region Private func
        private IEnumerator Flashing()
        {
            IsFlashing = true;
            while (flashValue > 0)
            {
                yield return null;
                flashValue -= 4.5f * Time.deltaTime;
                currentMaterial.SetFloat("FlashValue", flashValue);
            }
            IsFlashing = false;
        }
        #endregion
    }
}
