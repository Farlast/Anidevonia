using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class RuningState : GroundedState
    {
        public RuningState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState
        public override void Enter()
        {
            base.Enter();
            stateMachine.Player.PlayerAnimation.RunAnimation();
        }
        public override void Update()
        {
            base.Update();

            if (GetMoveInput() == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.StopingStateLight);
                return;
            }
            FlipSprite();
        }
        public override void FixUpdate()
        {
            base.FixUpdate();
            Move();
        }
        protected override void AddInputCallback()
        {
            base.AddInputCallback();
            stateMachine.Player.InputReader.JumpEvent += OnJump;
        }

        protected override void RemoveInputCallback()
        {
            base.RemoveInputCallback();
            stateMachine.Player.InputReader.JumpEvent -= OnJump;
        }
        #endregion

        #region Main
        private void OnJump(bool press)
        {
            stateMachine.ChangeState(stateMachine.JumpingState);
        }
        #endregion
    }
}
