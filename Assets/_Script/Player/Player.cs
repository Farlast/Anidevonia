using UnityEngine;
using Script.Core.Input;
using Script.Core;

namespace Script.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] internal StateData Data;
        [SerializeField] internal InputReader InputReader;

        internal PlayerAnimation PlayerAnimation;
        internal Rigidbody2D Rigidbody2D;
        private PlayerStateMachine movementStateMachine;
        internal PlayerHealth Health;
        internal bool IsGround { get; private set; }
        internal bool IsWallAtFront { get; private set; }

        [Space]
        [Header("[Collision]")]
        [SerializeField] private Transform feetPos;
        [SerializeField] private float checkRadiusFeetX;
        [SerializeField] private float checkRadiusFeetY;
        [SerializeField] private float eyeRadiusCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask iframeLayer;
        [SerializeField] private bool drawGismos;
        [Space]
        [Header("[Attack]")]
        [SerializeField] Transform attackPos;
        [SerializeField] Vector2 attackRange;
        [SerializeField] private Color HitboxColor;
        [SerializeField] LayerMask attackLayer;

        #region Main
        private void Awake()
        {
            movementStateMachine = new PlayerStateMachine(this);
            Rigidbody2D = GetComponent<Rigidbody2D>();
            PlayerAnimation = GetComponent<PlayerAnimation>();
            Health = GetComponent<PlayerHealth>();
        }
        void Start()
        {
            Data.Inint();
            Data.AttackData.ReSetEndComboCooldown();
            Data.DashData.ResetCooldown();
            movementStateMachine.ChangeState(movementStateMachine.Idling);
        }

        void Update()
        {
            GroundCheck();
            WallCheck();
            movementStateMachine.HandleInput();
            movementStateMachine.Update();
            InputReader.JumpBufferCalculation();
            InputReader.AttackBufferCalculation();
        }
        private void FixedUpdate()
        {
            movementStateMachine.FixUpdate();
        }
        private bool GroundCheck() => Data.JumpData.IsGround = Physics2D.OverlapBox(feetPos.position, new Vector2(checkRadiusFeetX, checkRadiusFeetY), 0f, groundLayer);
        private bool WallCheck() => Data.MovementData.IsWallAtFront = Physics2D.Raycast(transform.position, new Vector2(InputReader.LatesDirection.x, 0), eyeRadiusCheck, groundLayer);
        #endregion

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
        
        #region Debug
        private void OnDrawGizmos()
        {
            if (!drawGismos) return;

            float _latesDirection = InputReader ? InputReader.LatesDirection.x : 1;
           
            /// eye
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(_latesDirection * eyeRadiusCheck, 0, 0));
            
            if (feetPos != null)
            {
                // feet
                Gizmos.color = Color.cyan;
                Vector3 radius = new Vector3(checkRadiusFeetX, checkRadiusFeetY, 0);
                Gizmos.DrawWireCube(feetPos.position, radius);
            }
            if (attackPos != null)
            {
                Gizmos.color = HitboxColor;
                Gizmos.DrawCube(attackPos.position, new Vector2(attackRange.x, attackRange.y));
            }
        }
        #endregion
    }
}
