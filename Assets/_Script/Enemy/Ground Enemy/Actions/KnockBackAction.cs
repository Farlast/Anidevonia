namespace Script.Enemy
{
    public class KnockBackAction : Action
    {
        public KnockBackAction(EnemyBase enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.TakeHit = false;
            Agent.NewVector.Set(0, 0);
            Agent.SetVelocity(Agent.NewVector);
            Agent.EnemyAnimation.IdleAnimation();
        }
    }
}
