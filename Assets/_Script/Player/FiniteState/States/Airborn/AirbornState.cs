using UnityEngine;

namespace Script.Player
{
    public class AirbornState : MovementStateBase
    {
        public AirbornState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        protected void FallingStraight()
        {
            Vector2 downVector = new( 0, StateMachine.Player.Rigidbody2D.velocity.y);
            StateMachine.Player.Rigidbody2D.velocity = downVector;
        }
        protected override void OnAttack(bool press)
        {
            if (press && !Data.AttackData.IsCooldown)
            {
                StateMachine.ChangeState(StateMachine.AirAttack);
            }
        }
       protected virtual void HangOnAir()
        {
            ZeroGravity();
            ResetVelocity();
        }
    }
}
