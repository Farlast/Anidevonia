using UnityEngine;
using Script.Core.Input;
using Script.Core;

namespace Script.Player
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] InputReader InputReader;
        [SerializeField] Transform attackPosition;
        [SerializeField] Transform attackPos;
        [SerializeField] Vector2 attackRange;
        [SerializeField] private Color HitboxColor;
        [SerializeField] PlayerStats stats;
        [SerializeField] LayerMask attackLayer;
        [Header("Projectile")]
        [SerializeField] GameObject projectile;
        [SerializeField] ProjectilePool pool;

        internal bool IsAttackCooldown { get; private set; }
        internal bool IsAttackPress { get; private set; }
        internal bool IsHitTarget { get; private set; }
        internal bool IsCast { get; private set; }
        internal bool IsSkillAction { get; private set; }
        
        void Start()
        {
            InputReader.AttackEvent += OnAttack;
            InputReader.CastEvent += CastOrb;
            InputReader.SkillActionEvent += SkillAction;
        }
        private void OnDestroy()
        {
            InputReader.AttackEvent -= OnAttack;
            InputReader.CastEvent -= CastOrb;
            InputReader.SkillActionEvent -= SkillAction;
        }
        void OnAttack(bool action)
        {
            IsAttackPress = action;
        }
        void CastOrb()
        {
            FireProjectile();
        }
        void FireProjectile()
        {
            var proj = pool.GetAndSetPosition(attackPos);
            proj.SetUp(attackPos.position, InputReader.LatesDirection, 0.35f, pool);
        }
       
        void SkillAction()
        {
            print("SkillAction");
        }
        public void SetAttackCoolDown()
        {
            IsAttackCooldown = true;
            TimerSystem.Create(() => { IsAttackCooldown = false; }, stats.AttackEndComboTime);
        }
        public void Attack()
        {
            Collider2D[] TargetToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRange.x, attackRange.y), 0, attackLayer);

            if (TargetToDamage == null) return;

            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                IDamageable damageable = TargetToDamage[i].GetComponent<IDamageable>();

                if (damageable == null) { continue; }
                else
                {
                    IsHitTarget = true;
                    damageable.TakeDamage(new DamageInfo(stats.AttackDamage,transform.position));
                }
            }
        }
        #region Gizmos
        void OnDrawGizmos()
        {
            if (attackPos != null)
            {
                Gizmos.color = HitboxColor;
                Gizmos.DrawCube(attackPos.position, new Vector2(attackRange.x, attackRange.y));
            }
        }
        #endregion
    }
}
