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
            StateMachine.Player.PlayerAnimation.IdleAnimation();
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
            StateMachine.Player.InputReader.JumpEvent += OnJump;
        }

        protected override void RemoveInputCallback()
        {
            base.RemoveInputCallback();
            StateMachine.Player.InputReader.JumpEvent -= OnJump;
        }
        #endregion

        #region Main
        private void CheckSliping()
        {
            ResetVelocity();
        }
        private void CheckSwitchState()
        {
            if(GetMoveInput().x != 0)
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
