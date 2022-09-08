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
            StateMachine.Player.PlayerAnimation.LightStopAnimation();
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
            StateMachine.Player.InputReader.JumpEvent += OnJump;
        }

        protected override void RemoveInputCallback()
        {
            base.RemoveInputCallback();
            StateMachine.Player.InputReader.JumpEvent -= OnJump;
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
                StateMachine.ChangeState(StateMachine.Idling);
            }
            else if (IsMoveHorizontal())
            {
                StateMachine.ChangeState(StateMachine.Runing);
            }
        }
        private void OnJump(bool press)
        {
            StateMachine.ChangeState(StateMachine.JumpingState);
        }
        #endregion
        
    }
}
