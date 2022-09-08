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
            StateMachine.Player.InputReader.ResetAttackBuffer();
            animationFinish = false;
            StateMachine.Player.PlayerAnimation.AttackComboAnimation(combo);
            TimerSystem.Create(() => { animationFinish = true; WaitAfterAttackFinish(); },Data.AttackData.AttackSpeed);
            currentCombo++;
        }
        private void WaitAfterAttackFinish()
        {
            waitFinish = false;
            TimerSystem.Create(() => { waitFinish = true; }, Data.AttackData.WaitForNextAttakTime);
        }
        private void CheckNextState()
        {
            if (!animationFinish) return;

            if (StateMachine.Player.InputReader.IsAttackBuffering && currentCombo <= Data.AttackData.MaxCombo)
            {
                StartAttack(currentCombo);
                return;
            }

            if (IsMoveHorizontal())
            {
                Data.AttackData.SetEndComboCooldown();
                StateMachine.ChangeState(StateMachine.Runing);
                return;
            }
           
            if(waitFinish)
            {
                Data.AttackData.SetEndComboCooldown();
                StateMachine.ChangeState(StateMachine.Idling);
                return;
            }
            
            
        }
        #endregion
    }
}
