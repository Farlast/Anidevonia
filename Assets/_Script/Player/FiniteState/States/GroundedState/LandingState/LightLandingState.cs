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
            StateMachine.Player.PlayerAnimation.LightLandingAnimation();
        }
        public override void Update()
        {
            base.Update();

            CheckSwicthState();
            AnimationTimer();
        }
        private void AnimationTimer()
        {
            if(timer >= Data.JumpData.LandingAnimationTime)
            {
                finishAnimation = true;
            }
            timer += Time.deltaTime;
        }
        private void CheckSwicthState()
        {
            if (StateMachine.Player.InputReader.IsJumpBuffering)
            {
                StateMachine.ChangeState(StateMachine.JumpingState);
                StateMachine.Player.InputReader.ResetJumpBuffer();
                return;
            }
            if (IsMoveHorizontal())
            {
                StateMachine.ChangeState(StateMachine.Runing);
            }else if (finishAnimation)
            {
                StateMachine.ChangeState(StateMachine.Idling);
            }
        }
    }
}
