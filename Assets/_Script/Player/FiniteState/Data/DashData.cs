using System;
using UnityEngine;
using Script.Core;

namespace Script.Player
{
    [Serializable]
    public class DashData
    {
        [field: SerializeField] public bool IsCooldown { get; private set; }
        [field: SerializeField] public float DashSpeed { get; private set; }
        [field: SerializeField] public float DashTime { get; private set; }
        [field: SerializeField] public float BackDashTime { get; private set; }
        [field: SerializeField] public float DashCooldownTime { get; private set; }
        [field: SerializeField] public float StoredGravity { get; set; }

        public void ResetCooldown()
        {
            IsCooldown = false;
        }
        public void SetCoolDown()
        {
            IsCooldown = true;
            TimerSystem.Create(() => { IsCooldown = false; }, DashCooldownTime);
        }
    }
}
