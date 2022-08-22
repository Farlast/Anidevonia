using UnityEngine;
using System;

namespace Script.Core
{
    public interface IDamageable
    {
        void TakeDamage(DamageInfo info);
    }
    public enum KnockbackType
    {
        None,
        Stunt,
        Low,
        Heavy
    }

    [Serializable]
    public class DamageInfo
    {
        public DamageInfo(float damage, Vector3 attackerPosition)
        {
            Damage = damage;
            AttackerPosition = attackerPosition;
        }

        public float Damage { get; set; }
        public Vector3 AttackerPosition { get; set; }
        public KnockbackType KnockBack { get; set; }

        public float GetDirectionFromTarget(Vector3 currentPosition)
        {
            return (currentPosition - AttackerPosition).normalized.x;
        }
    }

    [Serializable]
    public class TakeDamageInfo
    {
        public bool IsDead;
        public DamageInfo DamageInfo;
    }
}
