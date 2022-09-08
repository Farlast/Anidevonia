using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/stats/Base stats")]
    public class Stats : ScriptableObject
    {
        [Header("Base")]
        public float MaxHP;
        public float MaxMp;
        public float Movespeed;
        public float JumpForce;
        public float MaxFallSpeed;
        public float MaxLiftUpSpeed;
        public float AttackDamage;
        public float AttackSpeed;
        public Material FlashMat;
        public GameObject TakeHitEffect;
        public float AttackKnockBack;
        public float StuntTime;
    }
}
