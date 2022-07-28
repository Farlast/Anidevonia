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
            if (Ctx.Combat.IsAttack)
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
            Ctx.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public override void OnStateRun()
        {
            if (!Ctx.IsGround) Ctx.AnimationPlayer.AirAnimation(Ctx.rb.velocity.y);
            CheckSwitchState();
        }

        public override void InitializeSubState()
        {
        }

        public override void OnStateFixedUpdate()
        {
            if(Ctx.IsGround && !Ctx.InputReader.IsJumpBuffering)
            {
                Ctx.rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            }
            else
            {
                Ctx.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            Ctx.NewVelocity.Set(0f, Ctx.rb.velocity.y);
            Ctx.rb.velocity = Ctx.NewVelocity;
        }
       
    }
}