using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public class Idle : State
    {
        [SerializeField] State MoveTo;
        public override void CheckSwitchState()
        {
            if (controller.View.SeeTarget)
            {
                SwicthState(MoveTo);
            }
        }

        public override void EnterState()
        {
            controller.Animator.IdleAnimation();
            controller.MoveVector.Set(0, 0);
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
