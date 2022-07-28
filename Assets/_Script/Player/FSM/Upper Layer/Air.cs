using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class Air : State
    {
        public Air(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
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
            else if (Ctx.IsGround)
            {
                SwitchState(_factory.Ground());
            }
            else if (Ctx.IsDash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
        }

        public override void InitializeSubState()
        {
            if(Ctx.IsMove)
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
            AddVelocity();
        }

        public override void OnStateRun()
        {
            CheckSwitchState();
        }
        void AddVelocity()
        {
            
            if (Ctx.rb.velocity.y < 0)
            {
                Ctx.rb.velocity += (Ctx.Stats.FallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
            else if (Ctx.rb.velocity.y > 0 && !Ctx.IsJumpPress)
            {
                Ctx.rb.velocity += (Ctx.Stats.LowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
        }
    }
}
