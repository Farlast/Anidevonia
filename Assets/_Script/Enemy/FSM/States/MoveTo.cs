using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public class MoveTo : State
    {
        [SerializeField] float CloseRange = 4f;
        [SerializeField] State MeleeAttack;
        [SerializeField] State LostPlayer;
        public override void CheckSwitchState()
        {
            if (controller.View.DistanceToTarget < CloseRange)
            {
                SwicthState(MeleeAttack);
            }
            else if (!controller.View.SeeTarget)
            {
                SwicthState(LostPlayer);
            }
        }

        public override void EnterState()
        {
            controller.Animator.RunAnimation();
        }

        public override void ExitState()
        {

        }

        public override void UpdateState()
        {
            Move();
            CheckSwitchState();
        }

        private void Move()
        {
            var targetDir = controller.View.DirectionToPlayer;
            controller.MoveVector.Set(controller.Enemy.Stats.Movespeed * targetDir.x, 0);
            controller.Enemy.FlipSprite(controller.LastDiraction);
        }
    }
}
