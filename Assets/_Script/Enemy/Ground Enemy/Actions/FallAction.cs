using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public class FallAction : Action
    {
        public FallAction(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void FixUpdate()
        {
            Agent.ResetVelocity();
            Agent.NewVector.Set(0, -10);
            Agent.SetVelocity(Agent.NewVector);
        }
        public override void OnExit()
        {
            base.OnExit();
            Agent.NewVector.Set(0,0);
        }
    }
}
