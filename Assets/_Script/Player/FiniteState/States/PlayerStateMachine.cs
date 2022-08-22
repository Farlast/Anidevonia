using UnityEngine;

namespace Script.Player
{
    public class PlayerStateMachine : StateMachine
    {
        public Player Player;

        // Any
        public DashingState DashingState { get; }
        public Knockback KnockBackState { get; }

        //ground
        public IdlingState Idling { get; }
        public RuningState Runing { get; }
        public LightStopingState StopingStateLight {get;}
        public LightLandingState LandingStateLight { get; }

        // Air
        public JumpingState JumpingState { get; }
        public FallingState FallingState { get; }

        public GroundAttack Attack { get; }
        public AirAttack AirAttack { get; }

        public Dead Dead { get; }

        public PlayerStateMachine(Player player)
        {
            Player = player;

            DashingState = new(this);
            KnockBackState = new(this);
            Dead = new(this);

            Idling = new (this);
            Runing = new (this);
            StopingStateLight = new (this);
            LandingStateLight = new (this);

            JumpingState = new(this);
            FallingState = new(this);

            Attack = new(this);
            AirAttack = new(this);
            
        }
    }
}
