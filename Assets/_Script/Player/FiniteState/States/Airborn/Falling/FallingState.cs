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
            StateMachine.Player.PlayerAnimation.AirAnimation(-1);
        }
        public override void Update()
        {
            base.Update();
            FlipSprite();

            if (IsGround())
            {
                StateMachine.ChangeState(StateMachine.LandingStateLight);
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
           
            NewVector.Set(StateMachine.Player.Rigidbody2D.velocity.x,
                Mathf.Clamp(StateMachine.Player.Rigidbody2D.velocity.y,
                Data.MovementData.MaxFallVelocity,
                Data.MovementData.MaxVelocity));

            StateMachine.Player.Rigidbody2D.velocity = NewVector;
        }
        #endregion
    }
}
