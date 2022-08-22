using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/stats/Player stats")]
    public class PlayerStats : Stats
    {
        [Header("--Player Upgrades--")]
        [Header("Flags")]
        public bool HasDoubleJump;
        public bool HasDash;
        public bool HasIframeDash;
        
        public float IframeTime;
        public float KnockBackTime;

        [Header("Skill")]
        public float AttackEndComboTime;
        public float MaxAttackCombo;

        [Header("Skill")]
        public bool CastOrb;
        public float OrbDecayTime;

    }
}
