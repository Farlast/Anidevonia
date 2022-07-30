using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/stats/Player stats")]
    public class PlayerStats : Stats
    {
        [Header("--Player--")]
        [Header("Flags")]
        public bool HasDoubleJump;
        public bool HasDash;
        public bool HasIframeDash;
        [Header("Dash")]
        public float DashSpeed;
        public float MaxDashTime;
        public float DashCooldown;
        [SerializeField] private AnimationCurve DashVelocity;
        [Header("Jump")]
        public float FallMultiplier;
        public float LowJumpMultiplier;

        public float IframeTime;

        public float KnockBackTime;
        [Header("Skill")]
        public float AttackEndComboTime;
        public float MaxAttackCombo;
        [Header("Skill")]
        public bool CastOrb;
        public float OrbDecayTime;

        public float GetDashVelocity(float dashCounter)
        {
            return DashVelocity.Evaluate(dashCounter / MaxDashTime);
        }
    }
}
