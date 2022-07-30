using UnityEngine;
using Script.Core;

namespace Script.Enemy
{
    public class Knockback : State
    {
        [SerializeField] State AfterFinish;
        bool finish;
        
        public override void CheckSwitchState()
        {
            if (finish)
            {
                SwicthState(AfterFinish);
            }
        }

        public override void EnterState()
        {
            finish = false;
            TimerSystem.Create(() => { finish = true; }, controller.Enemy.Stats.StuntTime);
            controller.Takehit = false;
            controller.MoveVector.Set(0, controller.Velocity.y);
        }

        public override void ExitState()
        {

        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}
