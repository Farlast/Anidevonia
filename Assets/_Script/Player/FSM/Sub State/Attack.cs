using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class Attack : State
    {
        public Attack(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
        }
        bool isFinish;
        void FinishAttack()
        {
            isFinish = true;
        }
        public override void CheckSwitchState()
        {
            if (isFinish)
            {
                SwitchState(_factory.Idle());
            }
            else if (Ctx.IsDash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
        }

        public override void OnStateEnter()
        {
            isFinish = false;
            Ctx.InputReader.ResetAttackBuffer();
            TimerSystem.Create(FinishAttack, Ctx.Stats.AttackSpeed, "attack");
            
            if(Ctx.MoveInputVector.y == 0)
            {
                Ctx.AnimationPlayer.AttackAnimation();
            }
            else if(Ctx.MoveInputVector.y > 0)
            {
                //up
                Ctx.AnimationPlayer.AttackUpAnimation();
            }
            else if(Ctx.MoveInputVector.y < 0 && !Ctx.IsGround)
            {
                //down
                Ctx.AnimationPlayer.AttackDownAnimation();
                if (Ctx.Combat.IsHitTarget)
                {
                    Ctx.NewVelocity.Set(0,20);
                    Ctx.rb.AddRelativeForce(Ctx.NewVelocity);
                }
            }
        }

        public override void OnStateExit()
        {
            Ctx.AnimationPlayer.AttackEffectOff();
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
            Ctx.Move();
        }
       
    }
}