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
            Vector2 downVector = new( 0, stateMachine.Player.Rigidbody2D.velocity.y);
            stateMachine.Player.Rigidbody2D.velocity = downVector;
        }
        protected override void OnAttack(bool press)
        {
            if (press && !ShareStateData.AttackData.IsCooldown)
            {
                stateMachine.ChangeState(stateMachine.AirAttack);
            }
        }
       protected virtual void HangOnAir()
        {
            ZeroGravity();
            ResetVelocity();
        }
    }
}
