using UnityEngine;

namespace Script.Player
{
    public class Idle : State
    {
        
        public Idle(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsCanGroundAttack())
            {
                SwitchState(_factory.Attack());
            }
            else if(Ctx.IsMove)
            {
                SwitchState(_factory.Move());
            }
        }

        public override void OnStateEnter()
        {
            if (Ctx.IsGround) Ctx.AnimationPlayer.IdleAnimation();
            else Ctx.AnimationPlayer.AirAnimation(Ctx.rb.velocity.y);
        }

        public override void OnStateExit()
        {

        }

        public override void OnStateRun()
        {
            CheckSwitchState();
        }

        public override void InitializeSubState()
        {

        }

        public override void OnStateFixedUpdate()
        {
            Ctx.NewVelocity.Set(0f, Ctx.rb.velocity.y);
            Ctx.rb.velocity = Ctx.NewVelocity;
        }
       
    }
}