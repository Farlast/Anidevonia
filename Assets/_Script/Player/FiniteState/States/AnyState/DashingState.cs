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
            Data.DashData.SetCoolDown();
        }
        public IEnumerator Dash(float time)
        {
            yield return Helpers.GetWait(time);

            if (IsGround())
            {
                StateMachine.ChangeState(StateMachine.StopingStateLight);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.FallingState);
            }
        }
        protected override void OnDash(bool press)
        {
        }
        private void DashTypeByInput()
        {
            if (IsMoveHorizontal())
            {
                StateMachine.Player.PlayerAnimation.DashAnimation();
                NewVector.Set(Data.DashData.DashSpeed * StateMachine.Player.InputReader.LatesDirection.x, 0);
                StateMachine.Player.Rigidbody2D.velocity = NewVector;
                StateMachine.Player.StartCoroutine(Dash(Data.DashData.DashTime));
            }
            else
            {
                StateMachine.Player.PlayerAnimation.DashAnimation();
                NewVector.Set(Data.DashData.DashSpeed * StateMachine.Player.InputReader.LatesDirection.x * -1, 0);
                StateMachine.Player.Rigidbody2D.velocity = NewVector;
                StateMachine.Player.StartCoroutine(Dash(Data.DashData.BackDashTime));
            }
        }
        protected override void AutoFallWhenNotground()
        {
        }

    }
}
