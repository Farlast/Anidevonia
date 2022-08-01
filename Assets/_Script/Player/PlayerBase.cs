using Script.Core.Input;
using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
    public class PlayerBase : MonoBehaviour, ISprite
    {
        [SerializeField] internal InputReader InputReader;
        [SerializeField] internal PlayerStats Stats;
        [SerializeField] internal PlayerAnimation AnimationPlayer;
        [SerializeField] internal Combat Combat;
        [SerializeField] internal PlayerHealth HealthSystem;

        private StateFactory factory;
        private float saveGravity;
        private bool wasAction;

        internal Rigidbody2D rb;
        internal State CurrentState;
        internal Vector2 NewVelocity;

        [field: SerializeField] internal Vector2 MoveInputVector { get; private set; }
        internal bool IsMove { get; private set; }
        internal bool IsGround { get; private set; }
        internal bool IsWallAtFront { get; private set; }
        internal bool IsJumpPress { get; private set; }
        internal bool IsDash { get; private set; }
        internal int JumpCount { get; private set; }

        [Space]
        [Header("[debug]")]
        [SerializeField] string currentState;
        [SerializeField] string currentSubState;
        [SerializeField] bool drawGismos;
        [SerializeField] internal Vector2 Velocity;

        [Space]
        [Header("[Collision]")]
        [SerializeField] private Transform feetPos;
        [SerializeField] private float checkRadiusFeetX;
        [SerializeField] private float checkRadiusFeetY;
        [SerializeField] private float eyeRadiusCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask iframeLayer;
        private bool GroundCheck() => IsGround = Physics2D.OverlapBox(feetPos.position, new Vector2(checkRadiusFeetX, checkRadiusFeetY), 0f, groundLayer);
        private bool WallCheck() => IsWallAtFront = Physics2D.Raycast(transform.position, new Vector2(InputReader.LatesDirection.x, 0), eyeRadiusCheck, groundLayer);

        public DashState DashStates;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            MoveInputVector = Vector2.zero;
            factory = new StateFactory(this);
            CurrentState = factory.Ground();
            CurrentState.OnStateEnter();

            JumpCount = 0;
            saveGravity = rb.gravityScale;

            InputReader.MoveEvent += OnMove;
            InputReader.JumpEvent += OnJump;
            InputReader.DashEvent += OnDash;
        }
        private void OnDestroy()
        {
            InputReader.MoveEvent -= OnMove;
            InputReader.JumpEvent -= OnJump;
            InputReader.DashEvent -= OnDash;
        }
        void Update()
        {
            currentState = CurrentState.GetType().Name;
            currentSubState = CurrentState.CurrentSubName;
            Velocity = rb.velocity;

            if (IsGround) JumpCount = 0;
            CurrentState.UpdateStates();
            GroundCheck();
            WallCheck();
            InputReader.JumpBufferCalculation();
            InputReader.AttackBufferCalculation();
        }
        private void FixedUpdate()
        {
            if (InputReader.IsJumpBuffering)
            {
                if (IsGround && DashStates != DashState.Dashing)
                {
                    // ground jump
                    DoJump();
                }
                else if (!IsGround && IsCanDoubleJump())
                {
                    // air jump
                    DoJump();
                }
            }
            CurrentState.FixedUpdateStates();
            NewVelocity.Set(rb.velocity.x, Mathf.Clamp(rb.velocity.y, Stats.MaxFallSpeed, Stats.MaxLiftUpSpeed));
            rb.velocity = NewVelocity;
        }
        #region Movement Input
        private void OnMove(Vector2 vector)
        {
            MoveInputVector = vector;
            IsMove = vector.x != 0;
        }
        private void OnJump(bool action)
        {
            IsJumpPress = action;
        }
        private void OnDash(bool action)
        {
            if (DashStates == DashState.Ready)
            {
                IsDash = action;
            }
            else
            {
                IsDash = false;
            }
        }

        #endregion
        #region Utility
        private void DoJump()
        {
            if (JumpCount >= 2 || wasAction) return;

            InputReader.ResetJumpBuffer();
            NewVelocity.Set(rb.velocity.x, Stats.JumpForce);
            rb.velocity = NewVelocity;
            JumpCount += 1;
            wasAction = true;
            TimerSystem.Create(() => { wasAction = false; }, 0.35f, "jump");
        }
        public bool IsCanDoubleJump()
        {
            return Stats.HasDoubleJump && JumpCount < 2;
        }
        public void Move()
        {
            if (isOnSlop && IsGround)
            {
                NewVelocity.Set((MoveInputVector.x * Stats.Movespeed * slopeNormal.x), rb.velocity.y * slopeNormal.y * Stats.Movespeed);
            }
            else
            {
                NewVelocity.Set((MoveInputVector.x * Stats.Movespeed), rb.velocity.y);
            }
            rb.velocity = NewVelocity;
        }
        public void FlipSprite()
        {
            transform.eulerAngles = InputReader.LatesDirection.x > 0 ? Vector3.zero : new Vector3(0, 180, 0);
        }
        public void RemoveGravity()
        {
            rb.gravityScale = 0;
        }
        public void RestoreGravity()
        {
            rb.gravityScale = saveGravity;
        }
        public void ChangeMat()
        {

        }
        #endregion
        #region Slope
        Vector2 slopeNormal;
        float slopeAngle;
        float slopeAngleOld;
        bool isOnSlop;
        public void MoveSlope()
        {
            var hit = Physics2D.Raycast(feetPos.position, Vector2.down, 0.5f, groundLayer);
            if (hit)
            {
                slopeNormal = Vector2.Perpendicular(hit.normal).normalized;
                slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                
                if(slopeAngle != slopeAngleOld)
                {
                    isOnSlop = true;
                }
                slopeAngleOld = slopeAngle;

                if (drawGismos)
                {
                    Debug.DrawRay(hit.point, slopeNormal, Color.cyan);
                    Debug.DrawRay(hit.point, hit.normal, Color.cyan);
                }
            }
        }
        #endregion
        #region Iframe
        public void SetIframeLayer(float time)
        {
            gameObject.layer = Helpers.Layermask_to_layer(iframeLayer);
            TimerSystem.Create(ResetLayer, time, "iframe");
        }
        private void ResetLayer()
        {
            gameObject.layer = Helpers.Layermask_to_layer(playerLayer);
        }
        #endregion
        #region Gizmos
        private void OnDrawGizmos()
        {
            if (!drawGismos) return;

            float _latesDirection = InputReader ? InputReader.LatesDirection.x : 1;
            if (Stats != null)
            {
                /// eye
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + new Vector3(_latesDirection * eyeRadiusCheck, 0, 0));
            }

            if (feetPos != null)
            {
                // feet
                Gizmos.color = Color.cyan;
                Vector3 radius = new Vector3(checkRadiusFeetX, checkRadiusFeetY, 0);
                Gizmos.DrawWireCube(feetPos.position, radius);
            }
        }
        #endregion
    }
}