using System;
using UnityEngine;
using Script.Core;

namespace Script.Player
{
    [Serializable]
    public class AttackData
    {
        [field: SerializeField] public bool IsCooldown { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }
        [field: SerializeField] public float WaitForNextAttakTime { get; private set; }

        [field: SerializeField] public int MaxCombo { get; private set; }
        [field: SerializeField] public float AttackMultiplyCombo2 { get; private set; }
        [field: SerializeField] public float AttackMultiplyCombo3 { get; private set; }
        [field: SerializeField] public float Combo1AnimationTime { get; private set; }
        [field: SerializeField] public float Combo2AnimationTime { get; private set; }
        [field: SerializeField] public float Combo3AnimationTime { get; private set; }

        [field: SerializeField] public float AirAttackHangTime { get; private set; }

        public void ReSetEndComboCooldown()
        {
            IsCooldown = false;
        }
        public void SetEndComboCooldown()
        {
            IsCooldown = true;
            TimerSystem.Create(() => { IsCooldown = false; }, AttackSpeed);
        }
    }
}
