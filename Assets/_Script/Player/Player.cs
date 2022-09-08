using UnityEngine;
using Script.Core.Input;
using Script.Core;

namespace Script.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] internal StateData Data;
        [SerializeField] internal InputReader InputReader;

        private PlayerStateMachine stateMachine;
        internal PlayerAnimation PlayerAnimation;
        internal Rigidbody2D Rigidbody2D;
        internal PlayerHealth Health;
        internal GroundCheck GroundCheck;
        internal AttackBox attackBox;

        internal bool IsWallAtFront { get; private set; }
        //[SerializeField] private float eyeRadiusCheck;

        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask iframeLayer;

        #region Main
        private void Awake()
        {
            stateMachine = new PlayerStateMachine(this);
            Rigidbody2D = GetComponent<Rigidbody2D>();
            PlayerAnimation = GetComponent<PlayerAnimation>();
            Health = GetComponent<PlayerHealth>();
            GroundCheck = GetComponent<GroundCheck>();
            attackBox = GetComponent<AttackBox>();
        }
        void Start()
        {
            Data.Inint();
            Data.AttackData.ReSetEndComboCooldown();
            Data.DashData.ResetCooldown();
            stateMachine.ChangeState(stateMachine.Idling);
            attackBox.SetDamageInfo(new DamageInfo(Data.AttackData.AttackDamage, transform.position) { KnockBack = KnockbackType.Low});
        }

        void Update()
        {
            stateMachine.HandleInput();
            stateMachine.Update();
            InputReader.JumpBufferCalculation();
            InputReader.AttackBufferCalculation();
        }
        private void FixedUpdate()
        {
            stateMachine.FixUpdate();
        }
        //private bool WallCheck() => Data.MovementData.IsWallAtFront = Physics2D.Raycast(transform.position, new Vector2(InputReader.LatesDirection.x, 0), eyeRadiusCheck, groundLayer);
        #endregion
        /*
        #region Combat
        public void Attack()
        {
            Collider2D[] TargetToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRange.x, attackRange.y), 0, attackLayer);

            if (TargetToDamage == null) return;

            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                
                if (!TargetToDamage[i].TryGetComponent<IDamageable>(out var damageable)) { continue; }
                
                damageable.TakeDamage(new DamageInfo(Data.AttackData.AttackDamage, transform.position));
                
            }
        }
        #endregion
        */
    }
}
