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

            StateMachine.Player.PlayerAnimation.AirAnimation(1);
            StateMachine.Player.InputReader.ResetJumpBuffer();
            ResetVelocity();
            NewVector.Set(0, Data.JumpData.JumpForce);
            StateMachine.Player.Rigidbody2D.AddForce(NewVector, ForceMode2D.Impulse);
        }
        public override void Update()
        {
            base.Update();
            FlipSprite();
            if (StateMachine.Player.Rigidbody2D.velocity.y < 0)
            {
                StateMachine.ChangeState(StateMachine.FallingState);
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
