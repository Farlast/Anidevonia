using UnityEngine;
using System.Collections;

namespace Script.Player
{
    class Move : State
    {       
        public Move(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.Combat.IsAttackPress && !Ctx.Combat.IsAttackCooldown)
            {
                SwitchState(_factory.Attack());
            }
            else if(!Ctx.IsMove)
            {
                SwitchState(_factory.Idle());
            }
        }

        public override void OnStateEnter()
        {
            if (Ctx.IsGround) Ctx.AnimationPlayer.RunAnimation();
            else Ctx.AnimationPlayer.AirAnimation(Ctx.rb.velocity.y);
        }
        public override void OnStateRun()
        {
            Ctx.FlipSprite();
            CheckSwitchState();
        }

        public override void OnStateExit()
        {

        }

        public override void InitializeSubState()
        {

        }
        private void UpdateMove()
        {
            Ctx.Move();
        }

        public override void OnStateFixedUpdate()
        {
            UpdateMove();
        }
    }
}
