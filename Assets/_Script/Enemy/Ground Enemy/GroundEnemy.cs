using UnityEngine;
using Script.Core;
using System;

namespace Script.Enemy
{
   
    public class GroundEnemy : EnemyBase
    {
        [Header("Ground Check")]
        [SerializeField] protected Transform GroundCheckPos;
        [SerializeField] protected Vector2 CheckRadius;
        [SerializeField] protected LayerMask GroundLayer;
        [SerializeField] protected bool IsGround;
        [Header("Front Check")]
        [SerializeField] protected bool IsWallAtFront;
        [SerializeField] protected float EyeRayLength;
        [Header("Combat")]
        [SerializeField] protected Transform AttackPos;
        [SerializeField] protected Vector2 AttackRange;
        [SerializeField] protected LayerMask AttackLayer;
        [SerializeField] protected float AttackDamage;

        public string CurrentState ="";
        private StateMachine stateMachine;

        IdleAction idle;

        #region RayCast
        [SerializeField] private bool drawGismos;
        [SerializeField] private Color HitboxColor;
        private bool GroundCheck() => IsGround = Physics2D.OverlapBox(GroundCheckPos.position, new Vector2(CheckRadius.x, CheckRadius.y), 0f, GroundLayer);
        private bool WallCheck() => IsWallAtFront = Physics2D.Raycast(transform.position, new Vector2(LastDiraction, 0), EyeRayLength, GroundLayer);
        #endregion

        #region override
       
        public override void Attack()
        {
            Collider2D[] TargetToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(AttackRange.x, AttackRange.y), 0, AttackLayer);

            if (TargetToDamage == null) return;

            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                if (!TargetToDamage[i].TryGetComponent<IDamageable>(out var damageable)) { continue; }
                damageable.TakeDamage(new DamageInfo(AttackDamage, transform.position) { KnockBack = KnockbackType.Low});
            }
        }
        #endregion

        #region SetUp
        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            SetUpStateMachine();
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.SetState(idle);
        }
        private void Update()
        {
            GroundCheck();
            WallCheck();
            stateMachine.Update();
            CurrentState = stateMachine.GetCurrentState();
        }
        private void FixedUpdate()
        {
            stateMachine.FixUpdate();
        }
        protected virtual void SetUpStateMachine()
        {
            stateMachine = new();
            
            idle = new IdleAction(this,stateMachine);
            var followTarget = new FollowTarget(this, stateMachine);
            var fall = new FallAction(this, stateMachine);
            var attack = new AttackAction(this, stateMachine);
            var knockback = new KnockBackAction(this, stateMachine);

            AddTransition(idle, followTarget, SeeTarget());

            AddTransition(followTarget, idle, NotSeeTarget());
            AddTransition(followTarget, attack, TargetInMeleeRange());

            AddAnyTransition( fall, NotGround());
            AddTransition(fall, idle, Ground());

            AddTransition(attack, idle, AttackFinish());

        }
        #endregion

        #region StateMachaine Decision

        Func<bool> SeeTarget() => () => EyeView.SeeTarget;
        Func<bool> NotSeeTarget() => () => !EyeView.SeeTarget;
        Func<bool> TargetInMeleeRange() => () => EyeView.DistanceToTarget < AttackRange.x +2f;
        Func<bool> NotGround() => () => !IsGround;
        Func<bool> Ground() => () => IsGround;
        Func<bool> AttackFinish() => () => IsAttackReady;
        #endregion

        #region Reuseable
        protected void AddTransition(IState from, IState to, Func<bool> decision)
        {
            stateMachine.AddTransition(from, to, decision);
        }
        protected void AddAnyTransition(IState to, Func<bool> decision)
        {
            stateMachine.AddAnyTransition(to, decision);
        }
        #endregion
        #region Debug
        private void OnDrawGizmos()
        {
            if (!drawGismos) return;

            float _latesDirection = 1;

            /// eye
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(_latesDirection * EyeRayLength, 0, 0));

            if (GroundCheckPos != null)
            {
                // feet
                Gizmos.color = Color.cyan;
                Vector3 radius = new Vector3(CheckRadius.x, CheckRadius.y, 0);
                Gizmos.DrawWireCube(GroundCheckPos.position, radius);
            }
            if (AttackPos != null)
            {
                Gizmos.color = HitboxColor;
                Gizmos.DrawCube(AttackPos.position, new Vector2(AttackRange.x, AttackRange.y));
            }
        }
        #endregion
    }
}
