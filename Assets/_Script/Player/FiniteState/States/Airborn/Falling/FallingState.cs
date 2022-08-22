using UnityEngine;

namespace Script.Player
{
    public class FallingState : AirbornState
    {
        public FallingState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState
        public override void Enter()
        {
            base.Enter();
            stateMachine.Player.PlayerAnimation.AirAnimation(-1);
        }
        public override void Update()
        {
            base.Update();
            FlipSprite();
            if (stateMachine.Player.Data.JumpData.IsGround)
            {
                stateMachine.ChangeState(stateMachine.LandingStateLight);
            }
        }
        public override void FixUpdate()
        {
            base.FixUpdate();
            if (GetMoveInput().x == 0)
            {
                FallingStraight();
            }
            else
            {
                Move();
            }
           
            NewVector.Set(stateMachine.Player.Rigidbody2D.velocity.x,
                Mathf.Clamp(stateMachine.Player.Rigidbody2D.velocity.y,
                ShareStateData.MovementData.MaxFallVelocity,
                ShareStateData.MovementData.MaxVelocity));

            stateMachine.Player.Rigidbody2D.velocity = NewVector;
        }
        #endregion
    }
}
