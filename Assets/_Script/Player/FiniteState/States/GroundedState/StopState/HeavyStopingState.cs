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
            currentMoveDirection = StateMachine.Player.InputReader.LatesDirection;
            StateMachine.Player.PlayerAnimation.LightStopAnimation();
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
                StateMachine.ChangeState(StateMachine.Idling);
            }
            if (!IsGround())
            {
                StateMachine.ChangeState(StateMachine.FallingState);
            }
        }
    }
}
