using Script.Core;
using UnityEngine;

namespace Script.Player
{
    public class AirAttack : AirbornState
    {
        public AirAttack(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        private bool animationFinish;

        #region IStage
        public override void Enter()
        {
            base.Enter();
            ResetVelocity();
            HangOnAir();
            StartAttack();
        }

        public override void Update()
        {
            base.Update();
            CheckNextState();
        }
        public override void Exit()
        {
            base.Exit();
            RestoreGravity();
        }
        #endregion

        #region Main
        private void StartAttack()
        {
            animationFinish = false;
            StateMachine.Player.PlayerAnimation.AttackAnimation();
            TimerSystem.Create(() => { animationFinish = true; }, Data.AttackData.AirAttackHangTime);
        }
        private void CheckNextState()
        {
            if (!animationFinish) return;

            if (StateMachine.Player.InputReader.IsAttackBuffering)
            {
                StateMachine.ChangeState(StateMachine.Attack);
                return;
            }

            Data.AttackData.SetEndComboCooldown();
            StateMachine.ChangeState(StateMachine.FallingState);
            
        }
        protected override void AutoFallWhenNotground()
        {
        }
        #endregion
    }
}
