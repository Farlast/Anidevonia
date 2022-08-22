using UnityEngine;
using Script.Core;

namespace Script.Player
{
    [CreateAssetMenu(menuName = "ScriptableObject/StateMachine/Data")]
    public class StateData : ScriptableObject
    {
        [Header("debug")]
        public string CurrentState;
        public string Log;
        public Vector2 Velocity;

        [field: SerializeField] public MovementData MovementData { get; private set; }
        [field: SerializeField] public JumpData JumpData { get; private set; }
        [field:SerializeField] public DashData DashData { get; private set; }
        [field: SerializeField] public AttackData AttackData { get; private set; }
        [field: SerializeField] public KnockBackData KnockBackData { get; private set; }
        [field: SerializeField] public TakeDamageInfo DamageTakenInfo { get; private set; }
        
        public void SetDamageInfo(TakeDamageInfo info)
        {
            DamageTakenInfo = info;
        }
        public void Inint()
        {
            DamageTakenInfo.IsDead = false;
            MovementData.MoveInput = Vector2.zero;
        }
    }
}
