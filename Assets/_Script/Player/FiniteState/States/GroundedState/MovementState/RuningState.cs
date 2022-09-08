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
            StateMachine.Player.PlayerAnimation.RunAnimation();
        }
        public override void Update()
        {
            base.Update();

            if (GetMoveInput().x == 0)
            {
                StateMachine.ChangeState(StateMachine.StopingStateLight);
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
            StateMachine.Player.InputReader.JumpEvent += OnJump;
        }

        protected override void RemoveInputCallback()
        {
            base.RemoveInputCallback();
            StateMachine.Player.InputReader.JumpEvent -= OnJump;
        }
        #endregion

        #region Main
        private void OnJump(bool press)
        {
            StateMachine.ChangeState(StateMachine.JumpingState);
        }
        #endregion
    }
}
