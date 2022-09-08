using UnityEngine;

namespace Script.Enemy
{
    public class IdleAction : Action
    {
        public IdleAction(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.ResetVelocity();
            Agent.EnemyAnimation.IdleAnimation();
        }
    }
}
