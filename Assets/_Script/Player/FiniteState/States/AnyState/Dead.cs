using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class Dead : MovementStateBase
    {
        public Dead(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        #region IState
        public override void Enter()
        {
            base.Enter();
            StateMachine.Player.PlayerAnimation.DeadAnimation();
            ResetVelocity();
            ZeroGravity();
        }

        protected override void AutoFallWhenNotground()
        {
        }
        protected override void AddInputCallback()
        {
        }
        protected override void RemoveInputCallback()
        {
        }
        public override void Exit()
        {
            base.Exit();
            RestoreGravity();
        }
        #endregion
    }
}
