using System.Collections;
using Script.Core;
using UnityEngine;

namespace Script.Player
{
    public class Knockback : MovementStateBase
    {
        public Knockback(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState
        public override void Enter()
        {
            base.Enter();
            GetKnockBack();
            stateMachine.Player.PlayerAnimation.KnockBackAnimation();
        }
        protected override void AutoFallWhenNotground()
        {
        }
        #endregion

        #region Main
        private void GetKnockBack()
        {
            ResetVelocity();
            Stunt();

            NewVector.Set(KnockBackValue() * KnockBackDirection(),KnockBackValue());
            
            stateMachine.Player.Rigidbody2D.AddForce(NewVector,ForceMode2D.Impulse);
        }
        private float KnockBackValue()
        {
            float value = 0;
            switch (ShareStateData.DamageTakenInfo.DamageInfo.KnockBack)
            {
                case KnockbackType.None:
                    value = 0;
                    break;
                case KnockbackType.Stunt:
                    value = 0;
                    break;
                case KnockbackType.Low:
                    value = ShareStateData.KnockBackData.KnockBackForceLow;
                    break;
                case KnockbackType.Heavy:
                    value = ShareStateData.KnockBackData.KnockBackForceHeavy;
                    break;
                default:
                    break;
            }
            return value;
        }
        private float KnockBackDirection()
        {
            return ShareStateData.DamageTakenInfo.DamageInfo.GetDirectionFromTarget(stateMachine.Player.transform.position);
        }
        private void Stunt()
        {
            TimerSystem.Create(() => { ChangeState(); },ShareStateData.KnockBackData.StuntTime);
        }
        private void ChangeState()
        {
            stateMachine.ChangeState(stateMachine.Idling);
        }
        protected override void AddInputCallback()
        {
        }
        protected override void RemoveInputCallback()
        {
        }
        #endregion
    }
}
