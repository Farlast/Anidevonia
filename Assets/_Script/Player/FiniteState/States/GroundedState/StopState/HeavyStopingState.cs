using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class HeavyStopingState : GroundedState
    {
        public HeavyStopingState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }
        private Vector2 currentMoveDirection;
        public override void Enter()
        {
            base.Enter();
            currentMoveDirection = stateMachine.Player.InputReader.LatesDirection;
            stateMachine.Player.PlayerAnimation.LightStopAnimation();
        }
        public override void FixUpdate()
        {
            base.FixUpdate();
            //DecelerateMovement(currentMoveDirection);
        }
        public override void Update()
        {
            base.Update();
            if (IsStopMoving())
            {
                stateMachine.ChangeState(stateMachine.Idling);
            }
            if (!ShareStateData.JumpData.IsGround)
            {
                stateMachine.ChangeState(stateMachine.FallingState);
            }
        }
    }
}
