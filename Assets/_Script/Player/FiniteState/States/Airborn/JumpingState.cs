using UnityEngine;

namespace Script.Player
{
    public class JumpingState : AirbornState
    {
        public JumpingState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState
        public override void Enter()
        {
            base.Enter();

            stateMachine.Player.PlayerAnimation.AirAnimation(1);
            stateMachine.Player.InputReader.ResetJumpBuffer();
            ResetVelocity();
            NewVector.Set(0, ShareStateData.JumpData.JumpForce);
            stateMachine.Player.Rigidbody2D.AddForce(NewVector, ForceMode2D.Impulse);
        }
        public override void Update()
        {
            base.Update();
            FlipSprite();
            if (stateMachine.Player.Rigidbody2D.velocity.y < 0)
            {
                stateMachine.ChangeState(stateMachine.FallingState);
                return;
            }
        }
        public override void FixUpdate()
        {
            base.FixUpdate();
            
            Move();
        }
        protected override void AutoFallWhenNotground()
        {
        }
        #endregion
    }
}
