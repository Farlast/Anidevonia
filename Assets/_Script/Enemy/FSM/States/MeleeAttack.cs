using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Core;

namespace Script.Enemy
{
    public class MeleeAttack : State
    {
        bool animationFinish = false;
        [SerializeField] State Oncomplete;

        public override void CheckSwitchState()
        {
            if (!controller.View.SeeTarget || animationFinish)
            {
                SwicthState(Oncomplete);
            }
        }

        public override void EnterState()
        {
            controller.MoveVector.Set(0, 0);
            animationFinish = false;
            controller.Animator.AttackAnimation();
            TimerSystem.Create(() => { animationFinish = true; }, 0.5f);
        }

        public override void ExitState()
        {

        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}
