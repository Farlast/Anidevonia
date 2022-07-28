using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class Dash : State
    {
        public Dash(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
        }
        
        private float dashTimer;
        private float dashDirection;
        public override void CheckSwitchState()
        {
            if (Ctx.DashStates == DashState.Cooldown)
            {
                if(Ctx.IsGround)
                    SwitchState(_factory.Ground());
                else
                    SwitchState(_factory.Ground());
            }
        }

        public override void InitializeSubState()
        {
            
        }
        // state switch and reEnter when base state change.
        public override void OnStateEnter()
        {
            if (Ctx.DashStates != DashState.Ready) return;
            if(Ctx.Stats.HasIframeDash) Ctx.SetIframeLayer(Ctx.Stats.MaxDashTime);
            Ctx.RemoveGravity();
            Ctx.FlipSprite();
            Ctx.AnimationPlayer.DashAnimation();
            dashDirection = Ctx.InputReader.LatesDirection.x;
        }

        public override void OnStateExit()
        {
            if (Ctx.DashStates == DashState.Dashing) return;
            Ctx.NewVelocity.Set(0, 0);
            Ctx.FlipSprite();
            Ctx.StartCoroutine(UpdateDash());
        }

        public override void OnStateFixedUpdate()
        {
            if(dashTimer <= Ctx.Stats.MaxDashTime && !Ctx.IsWallAtFront && !Ctx.HealthSystem.IsTakeDamage)
            {
                Ctx.NewVelocity.Set(Ctx.Stats.DashSpeed * dashDirection, 0);
                Ctx.rb.velocity = Ctx.NewVelocity;
                dashTimer += Time.deltaTime;
            }
            else
            {
                dashTimer = 0f;
                Ctx.DashStates = DashState.Cooldown;
            }
        }

        public override void OnStateRun()
        {
            CheckSwitchState();
        }
        IEnumerator UpdateDash()
        {
            Ctx.RestoreGravity();
            yield return Helpers.GetWait(Ctx.Stats.DashCooldown);
            Ctx.DashStates = DashState.Ready;
        }
    }
}