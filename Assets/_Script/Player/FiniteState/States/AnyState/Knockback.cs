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
            StateMachine.Player.PlayerAnimation.KnockBackAnimation();
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
            
            StateMachine.Player.Rigidbody2D.AddForce(NewVector,ForceMode2D.Impulse);
        }
        private float KnockBackValue()
        {
            float value = 0;
            switch (Data.DamageTakenInfo.DamageInfo.KnockBack)
            {
                case KnockbackType.None:
                    value = 0;
                    break;
                case KnockbackType.Stunt:
                    value = 0;
                    break;
                case KnockbackType.Low:
                    value = Data.KnockBackData.KnockBackForceLow;
                    break;
                case KnockbackType.Heavy:
                    value = Data.KnockBackData.KnockBackForceHeavy;
                    break;
                default:
                    break;
            }
            return value;
        }
        private float KnockBackDirection()
        {
            return Data.DamageTakenInfo.DamageInfo.GetDirectionFromTarget(StateMachine.Player.transform.position);
        }
        private void Stunt()
        {
            TimerSystem.Create(() => { ChangeState(); },Data.KnockBackData.StuntTime);
        }
        private void ChangeState()
        {
            StateMachine.ChangeState(StateMachine.Idling);
        }
        protected override void OnDash(bool press)
        {
        }
        protected override void OnAttack(bool press)
        {
        }
        #endregion
    }
}
