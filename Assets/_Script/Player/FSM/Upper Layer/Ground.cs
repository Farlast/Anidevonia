using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class Ground : State
    {
        public Ground(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Ctx.HealthSystem.IsTakeDamage)
            {
                SwitchState(_factory.KnockBack());
            }
            else if(!Ctx.IsGround)
            {
                SwitchState(_factory.Air());
            }
            else if (Ctx.IsDash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
        }

        public override void InitializeSubState()
        {
            if (Ctx.IsMove)
                SetSubState(_factory.Move());
            else
                SetSubState(_factory.Idle());
        }

        public override void OnStateEnter()
        {
            InitializeSubState();
        }

        public override void OnStateExit()
        {
            
        }

        public override void OnStateFixedUpdate()
        {
        }

        public override void OnStateRun()
        {
            CheckSwitchState();
        }
    }
}
