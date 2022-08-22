using UnityEngine;
using System.Collections;

namespace Script.Player
{
    public class LightStopingState : GroundedState
    {
        public LightStopingState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        private bool finishAnimation;
        private float timeCounter;

        #region IState
        public override void Enter()
        {
            base.Enter();
            finishAnimation = false;
            timeCounter = 0;
            stateMachine.Player.PlayerAnimation.LightStopAnimation();
            ResetVelocity();
        }
       
        public override void Update()
        {
            base.Update();

            SwicthState();
            WaitAnimationFinish();
        }
        protected override void AddInputCallback()
        {
            base.AddInputCallback();
            stateMachine.Player.InputReader.JumpEvent += OnJump;
        }

        protected override void RemoveInputCallback()
        {
            base.RemoveInputCallback();
            stateMachine.Player.InputReader.JumpEvent -= OnJump;
        }
        #endregion
        #region Main
        private void WaitAnimationFinish()
        {
            if(timeCounter >= 0.15f)
            {
                finishAnimation = true;
                SwicthState();
            }
            timeCounter += Time.deltaTime;
        }
        private void SwicthState()
        {
            if (finishAnimation)
            {
                stateMachine.ChangeState(stateMachine.Idling);
            }
            else if (IsMoveHorizontal())
            {
                stateMachine.ChangeState(stateMachine.Runing);
            }
        }
        private void OnJump(bool press)
        {
            stateMachine.ChangeState(stateMachine.JumpingState);
        }
        #endregion
        
    }
}
