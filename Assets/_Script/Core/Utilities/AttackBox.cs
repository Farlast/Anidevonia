using UnityEngine;

namespace Script.Core
{
    public class AttackBox : MonoBehaviour
    {
        public DamageInfo damageInfo;
        [SerializeField] private bool drawGismos;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private Vector2 attackRange;
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private Color HitboxColor;
        public void SetDamageInfo(DamageInfo info)
        {
            damageInfo = info;
        }
        public void Attack()
        {
            Collider2D[] TargetToDamage = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackRange.x, attackRange.y), 0, attackLayer);

            if (TargetToDamage == null) return;

            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                if (!TargetToDamage[i].TryGetComponent<IDamageable>(out var damageable)) { continue; }
                damageable.TakeDamage(damageInfo);
            }
        }
        private void OnDrawGizmos()
        {
            if (!drawGismos) return;
            if (attackPoint != null)
            {
                Gizmos.color = HitboxColor;
                Gizmos.DrawCube(attackPoint.position, new Vector2(attackRange.x, attackRange.y));
            }
        }
    }
}
