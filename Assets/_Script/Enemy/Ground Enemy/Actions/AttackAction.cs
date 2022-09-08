using System.Collections;
using UnityEngine;
using Script.Core;

namespace Script.Enemy
{
    public class AttackAction : Action
    {
        EnemyBase enemy;
        public AttackAction(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
            this.enemy = enemy;
        }

        public override void OnEnter()
        {
            StateMachine.StopUpdateTransition = true;
            base.OnEnter();
            Agent.ResetVelocity();
            
            enemy.StartCoroutine(Attack());           
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        IEnumerator Attack()
        {
            Agent.EnemyAnimation.PreAttackAnimation();
            yield return Helpers.GetWait(Agent.Data.Combat.PreAttackTime);
            Agent.EnemyAnimation.AttackAnimation();
            StateMachine.StopUpdateTransition = false;
            Agent.AttackCoolDown();
        }
    }
}
