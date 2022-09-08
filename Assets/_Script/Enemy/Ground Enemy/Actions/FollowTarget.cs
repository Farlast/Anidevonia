using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public class FollowTarget : Action
    {
        public FollowTarget(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void OnEnter()
        {
            // animation
            base.OnEnter();
        }

        public override void FixUpdate()
        {
            var targetDir = Agent.EyeView.DirectionToTarget;
            Agent.NewVector.Set(Agent.Data.Movement.Movespeed * targetDir.x, 0);
            Agent.SetVelocity(Agent.NewVector);
            Agent.FlipSprite(Agent.GetFaceingDiraction(Agent.NewVector));
        }
        public override void OnExit()
        {
            // animation
        }
    }
}
