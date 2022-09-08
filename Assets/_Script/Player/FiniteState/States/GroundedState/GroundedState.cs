using UnityEngine;

namespace Script.Player
{
    public class GroundedState : MovementStateBase
    {
        public GroundedState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        protected override void OnAttack(bool press)
        {
            if (press && !Data.AttackData.IsCooldown)
            {
                StateMachine.ChangeState(StateMachine.Attack);
            }
        }
    }
}
