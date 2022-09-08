using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class MovementStateBase : IState
    {
        protected PlayerStateMachine StateMachine;
        protected StateData Data;
        protected Vector2 NewVector;
        private Vector2 currentHorizontalVelovity;
        public MovementStateBase(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            Data = stateMachine.Player.Data;
        }

        #region IState
        public virtual void Enter()
        {
            AddInputCallback();
            Data.CurrentState = GetType().Name;
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
            Data.Velocity = StateMachine.Player.Rigidbody2D.velocity;
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
            if (Data.MovementData.MoveInput.x == 0 || Data.MovementData.SpeedMultiply == 0) return;

            NewVector.Set(Data.MovementData.MoveInput.x, 0);
            StateMachine.Player.Rigidbody2D.AddForce(NewVector * CalculateMoveSpeed() - GetCurrentHorizontalVelovity(), ForceMode2D.Impulse);
        }
        #endregion

        #region Reuseable
        protected bool IsGround()
        {
            return StateMachine.Player.GroundCheck.IsGround;
        }
        protected virtual void AutoFallWhenNotground()
        {
            if (!IsGround())
            {
                StateMachine.ChangeState(StateMachine.FallingState);
                return;
            }
        }
        protected void FlipSprite()
        {
            Vector3 rotation = GetMoveDiraction().x > 0 ? Vector3.zero : new Vector3(0, 180, 0);

            if (StateMachine.Player.transform.eulerAngles == rotation) return;
            StateMachine.Player.transform.eulerAngles = rotation;
        }
        protected Vector2 GetCurrentHorizontalVelovity()
        {
            currentHorizontalVelovity.Set(StateMachine.Player.Rigidbody2D.velocity.x,0f);
            return currentHorizontalVelovity;
        }
        protected Vector2 GetMoveInput()
        {
            return Data.MovementData.MoveInput;
        }
        protected bool IsMoveHorizontal()
        {
            return Data.MovementData.MoveInput.x != 0;
        }
        protected float CalculateMoveSpeed()
        {
            return Data.MovementData.MoveSpeed * Data.MovementData.SpeedMultiply;
        }
        protected void ResetVelocity()
        {
            StateMachine.Player.Rigidbody2D.velocity = Vector2.zero;
        }
        protected  Vector2 GetMoveDiraction()
        {
            return StateMachine.Player.InputReader.LatesDirection;
        }
        protected void DecelerateMovement(Vector2 MoveDirection)
        {
            if (IsStopMoving()) return;
            if (StateMachine.Player.Rigidbody2D.velocity.x > 0)
            {
                NewVector.Set(StateMachine.Player.Rigidbody2D.velocity.x - Data.MovementData.DecelerationSpeed, 0);
            }
            else
            {
                NewVector.Set(StateMachine.Player.Rigidbody2D.velocity.x + Data.MovementData.DecelerationSpeed, 0);
            }
            StateMachine.Player.Rigidbody2D.velocity = NewVector;
        }
        protected bool IsStopMoving()
        {
            if(GetMoveDiraction() == Vector2.left)
            {
                return IsInRange(-Data.MovementData.MinMoveVelocity, 0, StateMachine.Player.Rigidbody2D.velocity.x);
            }
            else
            {
                return IsInRange(0, Data.MovementData.MinMoveVelocity, StateMachine.Player.Rigidbody2D.velocity.x);
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
            Data.DashData.StoredGravity = StateMachine.Player.Rigidbody2D.gravityScale;
            StateMachine.Player.Rigidbody2D.gravityScale = 0;
        }
        protected void RestoreGravity()
        {
            StateMachine.Player.Rigidbody2D.gravityScale = Data.DashData.StoredGravity;
        }
        #endregion

        #region Callback
        protected virtual void AddInputCallback()
        {
            StateMachine.Player.InputReader.MoveEvent += OnMove;
            StateMachine.Player.InputReader.DashEvent += OnDash;
            StateMachine.Player.InputReader.AttackEvent += OnAttack;
            StateMachine.Player.Health.TakeDamageEvent += TakeDamage;
        }
        protected virtual void RemoveInputCallback()
        {
            StateMachine.Player.InputReader.MoveEvent -= OnMove;
            StateMachine.Player.InputReader.DashEvent -= OnDash;
            StateMachine.Player.InputReader.AttackEvent -= OnAttack;
            StateMachine.Player.Health.TakeDamageEvent -= TakeDamage;
        }
        #endregion

        #region Input
        void OnMove(Vector2 moveInput)
        {
            Data.MovementData.MoveInput = moveInput;
        }
        protected virtual void OnDash(bool press)
        {
            if(press && !Data.DashData.IsCooldown)
            StateMachine.ChangeState(StateMachine.DashingState);
        }
        protected virtual void OnAttack(bool press)
        {
            if (press && !Data.AttackData.IsCooldown)
            {
                StateMachine.ChangeState(StateMachine.Attack);
            }
        }
        protected virtual void TakeDamage(TakeDamageInfo info)
        {
            Data.SetDamageInfo(info);
            
            if (info.IsDead)
            {
                StateMachine.ChangeState(StateMachine.Dead);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.KnockBackState);
            }
        }
        #endregion
    }
}
