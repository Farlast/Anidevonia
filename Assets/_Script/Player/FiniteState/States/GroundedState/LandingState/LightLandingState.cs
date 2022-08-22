using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class LightLandingState : GroundedState
    {
        public LightLandingState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        private bool finishAnimation;
        private float timer;
        public override void Enter()
        {
            base.Enter();
            finishAnimation = false;
            timer = 0;
            stateMachine.Player.PlayerAnimation.LightLandingAnimation();
        }
        public override void Update()
        {
            base.Update();

            CheckSwicthState();
            AnimationTimer();
        }
        private void AnimationTimer()
        {
            if(timer >= ShareStateData.JumpData.LandingAnimationTime)
            {
                finishAnimation = true;
            }
            timer += Time.deltaTime;
        }
        private void CheckSwicthState()
        {
            if (stateMachine.Player.InputReader.IsJumpBuffering)
            {
                stateMachine.ChangeState(stateMachine.JumpingState);
                stateMachine.Player.InputReader.ResetJumpBuffer();
                return;
            }
            if (IsMoveHorizontal())
            {
                stateMachine.ChangeState(stateMachine.Runing);
            }else if (finishAnimation)
            {
                stateMachine.ChangeState(stateMachine.Idling);
            }
        }
    }
}
