using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class Attack : State
    {
        public Attack(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
        }
        bool isComboableAttack;
        bool isCurrentAnimationFinish;
        bool isEndAttack;
        int attackCombo;
        float attackTimeout = 0.5f;

        public override void CheckSwitchState()
        {

            if (Ctx.IsDash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
            if (!isEndAttack) return;

            if (Ctx.IsMove)
            {
                SwitchState(_factory.Move());
            }
            else
            {
                SwitchState(_factory.Idle());
            }
        }

        public override void OnStateEnter()
        {
            Reset();
            // first attack
            DiractionAttack();
            TimerSystem.Create(FinishAttack, Ctx.Stats.AttackSpeed, "Attack");
            
            if (Ctx.IsGround)
            {
                Ctx.NewVelocity.Set(0, 0);
                Ctx.rb.velocity = Ctx.NewVelocity;
            }
        }

        public override void OnStateExit()
        {
            Reset();
        }

        public override void OnStateRun()
        {
            CheckSwitchState();
            CheckAttackNext();
        }

        public override void InitializeSubState()
        {
           
        }
       
        public override void OnStateFixedUpdate()
        {

        }
        private void CheckAttackNext()
        {
            if (!isComboableAttack) return;
            if (!isCurrentAnimationFinish) return;

            if (Ctx.IsMove) 
            { 
                isEndAttack = true;
                Ctx.Combat.SetAttackCoolDown();
                return;
            }

            if (Ctx.Stats.MaxAttackCombo < attackCombo)
            {
                isEndAttack = true;
                Ctx.Combat.SetAttackCoolDown();
                return;
            }
            if (!isEndAttack && Ctx.InputReader.IsAttackBuffering && isCurrentAnimationFinish)
            {
                DoAttack();
                SetNextAttackWaitTimeout();
            }
        }
        private void DoAttack()
        {
           
            attackCombo++;
            Ctx.InputReader.ResetAttackBuffer();

            isCurrentAnimationFinish = false;
            TimerSystem.Create(FinishAttack, Ctx.Stats.AttackSpeed, "Attack");

            Ctx.AnimationPlayer.AttackComboAnimation(attackCombo);
        }
        private void DiractionAttack()
        {
            attackCombo++;
            if (Ctx.MoveInputVector.y == 0)
            {
                isComboableAttack = true;
                Ctx.AnimationPlayer.AttackAnimation();
            }
            else if (Ctx.MoveInputVector.y > 0)//UP
            {
                isComboableAttack = false;
                Ctx.AnimationPlayer.AttackUpAnimation();
            }
            Ctx.InputReader.ResetAttackBuffer();
        }
        
        void FinishAttack()
        {
            isCurrentAnimationFinish = true;

            if (isComboableAttack)
                SetNextAttackWaitTimeout();
            else
                isEndAttack = true;
            
        }
        private void SetNextAttackWaitTimeout()
        {
            TimerSystem.Create(() => { isEndAttack = true; }, attackTimeout, "AttackTimeout"); // addtime
        }
        private void Reset()
        {
            isEndAttack = false;
            isComboableAttack = false;
            isCurrentAnimationFinish = false;
            attackCombo = 0;
            
            Ctx.AnimationPlayer.AttackEffectOff();
        }
    }
}
/*sudo:
 - first attack press
 - count to end animation 
 - count combo timeout
if press attack and animation end
combo++ 
else 
end and exit

*/
