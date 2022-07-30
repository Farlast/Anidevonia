using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class KnockBack : State
    {
        public KnockBack(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }

        private bool finish;
        public override void CheckSwitchState()
        {
            if (!finish) return;

            /*if (Ctx.HealthSystem.IsDead)
            {
                SwitchState(_factory.Die());
            }*/
            if (Ctx.IsGround)
            {
                SwitchState(_factory.Ground());
            }
            else if(!Ctx.IsGround)
            {
                SwitchState(_factory.Air());
            }
        }

        public override void InitializeSubState()
        {

        }

        public override void OnStateEnter()
        {
            InitializeSubState();

            finish = false;
            var info = Ctx.HealthSystem.GetDamageInfo();
            Ctx.AnimationPlayer.KnockBackAnimation();
            Ctx.rb.velocity = Vector2.zero;
            Ctx.rb.inertia = 0;
            Ctx.rb.AddForce(CalculateKnockbackTaken(info), ForceMode2D.Impulse);
            TimerSystem.Create(() => { finish = true;  }, Ctx.Stats.KnockBackTime,"KnockBack");
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
        private Vector2 CalculateKnockbackTaken(DamageInfo damage)
        {
            if (damage.KnockBack > 0)
            {
                Vector2 direction = (Ctx.transform.position - damage.AttackerPosition).normalized;

               return  new Vector2(direction.x * damage.KnockBack, 1 * damage.KnockBack);
            }
            return Vector2.zero;
        }
    }
}
