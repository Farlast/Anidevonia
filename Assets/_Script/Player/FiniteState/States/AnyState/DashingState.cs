using UnityEngine;
using System.Collections;

namespace Script.Player
{
    public class DashingState : MovementStateBase
    {
        public DashingState(PlayerStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ZeroGravity();
            DashTypeByInput();
        }
        public override void Exit()
        {
            base.Exit();
            ResetVelocity();
            RestoreGravity();
            ShareStateData.DashData.SetCoolDown();
        }
        public IEnumerator Dash(float time)
        {
            yield return Helpers.GetWait(time);

            if (ShareStateData.JumpData.IsGround)
            {
                stateMachine.ChangeState(stateMachine.StopingStateLight);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.FallingState);
            }
        }
        protected override void OnDash(bool press)
        {
        }
        private void DashTypeByInput()
        {
            if (IsMoveHorizontal())
            {
                stateMachine.Player.PlayerAnimation.DashAnimation();
                NewVector.Set(ShareStateData.DashData.DashSpeed * stateMachine.Player.InputReader.LatesDirection.x, 0);
                stateMachine.Player.Rigidbody2D.velocity = NewVector;
                stateMachine.Player.StartCoroutine(Dash(ShareStateData.DashData.DashTime));
            }
            else
            {
                stateMachine.Player.PlayerAnimation.DashAnimation();
                NewVector.Set(ShareStateData.DashData.DashSpeed * stateMachine.Player.InputReader.LatesDirection.x * -1, 0);
                stateMachine.Player.Rigidbody2D.velocity = NewVector;
                stateMachine.Player.StartCoroutine(Dash(ShareStateData.DashData.BackDashTime));
            }
        }
        protected override void AutoFallWhenNotground()
        {
        }

    }
}
