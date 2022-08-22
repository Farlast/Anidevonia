using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class MovementStateBase : IState
    {
        protected PlayerStateMachine stateMachine;
        protected StateData ShareStateData;
        protected Vector2 NewVector;
        private Vector2 currentHorizontalVelovity;
        public MovementStateBase(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            ShareStateData = stateMachine.Player.Data;
        }

        #region IState
        public virtual void Enter()
        {
            AddInputCallback();
            ShareStateData.CurrentState = GetType().Name;
        }

        public virtual void Exit()
        {
            RemoveInputCallback();
        }
        public virtual void Update()
        {
            AutoFallWhenNotground();
        }

        public virtual void FixUpdate()
        {
            ShareStateData.Velocity = stateMachine.Player.Rigidbody2D.velocity;
        }

        public virtual void HandleInput()
        {
        }

        public virtual void OnAnimationEventEnter()
        {
            
        }

        public virtual void OnAnimationEventExit()
        {
            
        }

        public virtual void OnAnimationTransitionEvent()
        {
            
        }

        #endregion

        #region Main
       
        protected void Move()
        {
            if (ShareStateData.MovementData.MoveInput.x == 0 || ShareStateData.MovementData.SpeedMultiply == 0) return;

            NewVector.Set(ShareStateData.MovementData.MoveInput.x, 0);
            stateMachine.Player.Rigidbody2D.AddForce(NewVector * CalculateMoveSpeed() - GetCurrentHorizontalVelovity(), ForceMode2D.Impulse);
        }
        #endregion

        #region Reuseable
        protected virtual void AutoFallWhenNotground()
        {
            if (!ShareStateData.JumpData.IsGround)
            {
                stateMachine.ChangeState(stateMachine.FallingState);
                return;
            }
        }
        protected void FlipSprite()
        {
            Vector3 rotation = GetMoveDiraction().x > 0 ? Vector3.zero : new Vector3(0, 180, 0);

            if (stateMachine.Player.transform.eulerAngles == rotation) return;
            stateMachine.Player.transform.eulerAngles = rotation;
        }
        protected Vector2 GetCurrentHorizontalVelovity()
        {
            currentHorizontalVelovity.Set(stateMachine.Player.Rigidbody2D.velocity.x,0f);
            return currentHorizontalVelovity;
        }
        protected Vector2 GetMoveInput()
        {
            return ShareStateData.MovementData.MoveInput;
        }
        protected bool IsMoveHorizontal()
        {
            return ShareStateData.MovementData.MoveInput.x != 0;
        }
        protected float CalculateMoveSpeed()
        {
            return ShareStateData.MovementData.MoveSpeed * ShareStateData.MovementData.SpeedMultiply;
        }
        protected void ResetVelocity()
        {
            stateMachine.Player.Rigidbody2D.velocity = Vector2.zero;
        }
        protected  Vector2 GetMoveDiraction()
        {
            return stateMachine.Player.InputReader.LatesDirection;
        }
        protected void DecelerateMovement(Vector2 MoveDirection)
        {
            if (IsStopMoving()) return;
            if (stateMachine.Player.Rigidbody2D.velocity.x > 0)
            {
                NewVector.Set(stateMachine.Player.Rigidbody2D.velocity.x - ShareStateData.MovementData.DecelerationSpeed, 0);
            }
            else
            {
                NewVector.Set(stateMachine.Player.Rigidbody2D.velocity.x + ShareStateData.MovementData.DecelerationSpeed, 0);
            }
            stateMachine.Player.Rigidbody2D.velocity = NewVector;
        }
        protected bool IsStopMoving()
        {
            if(GetMoveDiraction() == Vector2.left)
            {
                return IsInRange(-ShareStateData.MovementData.MinMoveVelocity, 0, stateMachine.Player.Rigidbody2D.velocity.x);
            }
            else
            {
                return IsInRange(0, ShareStateData.MovementData.MinMoveVelocity, stateMachine.Player.Rigidbody2D.velocity.x);
            }
        }
        private bool IsInRange(float from,float to,float value)
        {
            // -1      0       1
            // from < value < to
            if (value < from) return false;
            if (value > to) return false;

            return true;

        }
        protected void ZeroGravity()
        {
            ShareStateData.DashData.StoredGravity = stateMachine.Player.Rigidbody2D.gravityScale;
            stateMachine.Player.Rigidbody2D.gravityScale = 0;
        }
        protected void RestoreGravity()
        {
            stateMachine.Player.Rigidbody2D.gravityScale = ShareStateData.DashData.StoredGravity;
        }
        #endregion

        #region Callback
        protected virtual void AddInputCallback()
        {
            stateMachine.Player.InputReader.MoveEvent += OnMove;
            stateMachine.Player.InputReader.DashEvent += OnDash;
            stateMachine.Player.InputReader.AttackEvent += OnAttack;
            stateMachine.Player.Health.TakeDamageEvent += TakeDamage;
        }
        protected virtual void RemoveInputCallback()
        {
            stateMachine.Player.InputReader.MoveEvent -= OnMove;
            stateMachine.Player.InputReader.DashEvent -= OnDash;
            stateMachine.Player.InputReader.AttackEvent -= OnAttack;
            stateMachine.Player.Health.TakeDamageEvent -= TakeDamage;
        }
        #endregion

        #region Input
        void OnMove(Vector2 moveInput)
        {
            ShareStateData.MovementData.MoveInput = moveInput;
        }
        protected virtual void OnDash(bool press)
        {
            if(press && !ShareStateData.DashData.IsCooldown)
            stateMachine.ChangeState(stateMachine.DashingState);
        }
        protected virtual void OnAttack(bool press)
        {
            if (press && !ShareStateData.AttackData.IsCooldown)
            {
                stateMachine.ChangeState(stateMachine.Attack);
            }
        }
        protected virtual void TakeDamage(TakeDamageInfo info)
        {
            ShareStateData.SetDamageInfo(info);
            
            if (info.IsDead)
            {
                stateMachine.ChangeState(stateMachine.Dead);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.KnockBackState);
            }
        }
        #endregion
    }
}
