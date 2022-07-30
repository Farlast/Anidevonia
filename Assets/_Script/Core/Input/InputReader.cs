using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Script.Core.Input
{
    [CreateAssetMenu(menuName = "ScriptableObject/Input Reader")]
    public class InputReader : ScriptableObject
    {
        public event UnityAction<Vector2> MoveEvent;
        public event UnityAction<bool> JumpEvent;
        public event UnityAction<bool> DashEvent;
        public event UnityAction<bool> AttackEvent;
        public event UnityAction InteractEvent;
        public event UnityAction CastEvent;
        public event UnityAction SkillActionEvent;
        // Pause menu
        public event UnityAction PauseEvent;
        public event UnityAction InventoryEvent;
        //Flags
        [field: SerializeField] public bool IsAttackBuffering { get; private set; }
        [field: SerializeField] public bool IsJumpBuffering { get; private set; }
        
        [SerializeField] private float attackbufferTime = 0.2f;
        [SerializeField] private float attackBufferingTimeCounter;
        [SerializeField] private float jumpbufferTime = 0.2f;
        [SerializeField] private float jumpBufferingTimeCounter;

        public Vector2 LatesDirection = Vector2.right;
        public float OldDirection;
        public void OnMove(InputAction.CallbackContext callback)
        {
            if(callback.phase == InputActionPhase.Canceled)
            {
                Vector2 move = callback.ReadValue<Vector2>();
                MoveEvent?.Invoke(Vector2.zero);
            }

            if (callback.phase == InputActionPhase.Performed)
            {
                Vector2 move =  callback.ReadValue<Vector2>();

                move.x = RoundUpInput(move.x);
                move.y = RoundUpInput(move.y);

                if(move.x == 0)
                {
                    LatesDirection.x = OldDirection;
                    LatesDirection = OldDirection > 0 ? Vector2.right : Vector2.left;
                }
                else
                {
                    OldDirection = move.x;
                    LatesDirection = move.x > 0 ?  Vector2.right : Vector2.left;
                }
                MoveEvent?.Invoke(move);
            }
        }
        private float RoundUpInput(float input)
        {
            if (input > -0.5f && input < 0.5f) input = 0;
            else
            {
                if (input > 0) input = 1;
                else if (input < 0 ) input = -1;
            }

            return input;
        }
        public void OnDash(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                DashEvent?.Invoke(true);
            }
            if (callback.phase == InputActionPhase.Canceled)
            {
                DashEvent?.Invoke(false);
            }
        }
        public void OnJump(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                JumpEvent?.Invoke(true);
                jumpBufferingTimeCounter = jumpbufferTime;
            }
            if (callback.phase == InputActionPhase.Canceled)
            {
                JumpEvent?.Invoke(false);
            }
        }
        public void OnAttack(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                AttackEvent?.Invoke(true);
                attackBufferingTimeCounter = attackbufferTime;
            }
            if (callback.phase == InputActionPhase.Canceled)
            {
                AttackEvent?.Invoke(false);
            }
        }
        public void OnInteract(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                InteractEvent?.Invoke();
            }
        }
        public void OnPause(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
            }
        }
        public void OnInventory(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                InventoryEvent?.Invoke();
            }
        }
        public void OnCast(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                CastEvent?.Invoke();
            }
        }
        public void OnSkillAction(InputAction.CallbackContext callback)
        {
            if (callback.phase == InputActionPhase.Performed)
            {
                SkillActionEvent?.Invoke();
            }
        }
        public bool IsDeviceMouseActive(InputAction.CallbackContext callback) => callback.control.device.name == "Mouse";
        
        #region Buffering
        // prees action before time
        // count until expire
        // or action are taken.
        public void AttackBufferCalculation()
        {

            if (attackBufferingTimeCounter > 0f)
            {
                attackBufferingTimeCounter -= Time.deltaTime;
                IsAttackBuffering = true;
            }
            else
            {
                IsAttackBuffering = false;
            }
        }
        public void ResetAttackBuffer()
        {
            attackBufferingTimeCounter = 0;
        }
        public void JumpBufferCalculation()
        {
            if (jumpBufferingTimeCounter > 0f)
            {
                jumpBufferingTimeCounter -= Time.deltaTime;
                IsJumpBuffering = true;
            }
            else
            {
                IsJumpBuffering = false;
            }
        }
        public void ResetJumpBuffer()
        {
            jumpBufferingTimeCounter = 0;
        }
        #endregion
    }
}
