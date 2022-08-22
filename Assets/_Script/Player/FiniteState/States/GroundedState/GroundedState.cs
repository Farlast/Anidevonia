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
            if (press && !ShareStateData.AttackData.IsCooldown)
            {
                stateMachine.ChangeState(stateMachine.Attack);
            }
        }
    }
}
