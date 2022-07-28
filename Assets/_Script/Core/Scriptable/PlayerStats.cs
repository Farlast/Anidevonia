using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/stats/Player stats")]
    public class PlayerStats : Stats
    {
        [Header("Player")]
        public bool HasDoubleJump;
        public bool HasDash;
        public bool HasIframeDash;
        public float DashSpeed;
        public float MaxDashTime;
        public float DashCooldown;
        public float FallMultiplier;
        public float LowJumpMultiplier;
        public float IframeTime;
        public float KnockBackTime;
        [Header("Skill")]
        public bool CastOrb;
        public float OrbDecayTime;
    }
}
