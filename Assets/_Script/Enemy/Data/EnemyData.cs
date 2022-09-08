using System.Collections;
using System;
using UnityEngine;

namespace Script.Enemy
{
    public enum Direction
    {
        Left,
        Right
    }
    [CreateAssetMenu(menuName = "ScriptableObject/Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [field:SerializeField] public HealthData Health { get; private set; }
        [field: SerializeField] public MovementData Movement { get; private set; }
        [field: SerializeField] public CombatData Combat { get; private set; }
        [field: SerializeField] public MaterialData Material { get; private set; }
    }
    [Serializable] public class HealthData
    {
        public float MaxHP;
        public int MaxSuperArmor;
    }
    [Serializable] public class MovementData
    {
        public float Movespeed;
        public Direction StartDirection;
        public float FallSpeed;
    }

    [Serializable] public class CombatData
    {
        public LayerMask AttackLayer;
        public float AttackSpeed;
        public float PreAttackTime;
        public float PostAttackTime;
        public float AttackDamage;
        public float AttackKnockback;
        public float StuntTime;
    }

    [Serializable] public class MaterialData
    {
        public Material FlashMat;
    }
}
