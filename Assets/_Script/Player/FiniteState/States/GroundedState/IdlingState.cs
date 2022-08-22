using UnityEngine;

namespace Script.Player
{
    public class IdlingState : GroundedState
    {
        public IdlingState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        #region IState
        public override void Enter()
        {
            base.Enter();

            ResetVelocity();
            stateMachine.Player.PlayerAnimation.IdleAnimation();
        }
        public override void Update()
        {
            base.Update();
            CheckSwitchState();
            CheckSliping();
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
        private void CheckSliping()
        {
            ResetVelocity();
        }
        private void CheckSwitchState()
        {
            if(GetMoveInput() != Vector2.zero)
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
