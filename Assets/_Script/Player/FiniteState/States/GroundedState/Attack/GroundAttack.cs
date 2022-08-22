using System.Collections;
using Script.Core;
using UnityEngine;

namespace Script.Player
{
    public class GroundAttack : GroundedState
    {
        public GroundAttack(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        private bool animationFinish;
        private bool waitFinish;
        private int currentCombo;
        #region IStage
        public override void Enter()
        {
            currentCombo = 1;
            waitFinish = false;
            base.Enter();
            ResetVelocity();
            StartAttack(currentCombo);
        }

        public override void Update()
        {
            base.Update();
            CheckNextState();
        }
        #endregion

        #region Main
        private void StartAttack(int combo)
        {
            stateMachine.Player.InputReader.ResetAttackBuffer();
            animationFinish = false;
            stateMachine.Player.PlayerAnimation.AttackComboAnimation(combo);
            TimerSystem.Create(() => { animationFinish = true; WaitAfterAttackFinish(); },ShareStateData.AttackData.AttackSpeed);
            currentCombo++;
        }
        private void WaitAfterAttackFinish()
        {
            waitFinish = false;
            TimerSystem.Create(() => { waitFinish = true; }, ShareStateData.AttackData.WaitForNextAttakTime);
        }
        private void CheckNextState()
        {
            if (!animationFinish) return;

            if (stateMachine.Player.InputReader.IsAttackBuffering && currentCombo <= ShareStateData.AttackData.MaxCombo)
            {
                StartAttack(currentCombo);
                return;
            }

            if (IsMoveHorizontal())
            {
                ShareStateData.AttackData.SetEndComboCooldown();
                stateMachine.ChangeState(stateMachine.Runing);
                return;
            }
           
            if(waitFinish)
            {
                ShareStateData.AttackData.SetEndComboCooldown();
                stateMachine.ChangeState(stateMachine.Idling);
                return;
            }
            
            
        }
        #endregion
    }
}
