using System.Collections.Generic;
namespace Script.Player
{
    public class StateFactory
    {
        PlayerBase _context;
        enum States
        {
            Ground,
            Air,
            Idle,
            Move,
            Jump,
            Attack,
            SkillAction,
            Dash,
            KnockBack
        }

        public StateFactory(PlayerBase context)
        {
            _context = context;
            _stateDictionary.Clear();
            _stateDictionary.Add(States.Idle, new Idle(_context, this));
            _stateDictionary.Add(States.Move, new Move(_context, this));
            _stateDictionary.Add(States.Ground, new Ground(_context, this));
            _stateDictionary.Add(States.Air, new Air(_context, this));
            _stateDictionary.Add(States.Attack, new Attack(_context, this));
            _stateDictionary.Add(States.SkillAction, new SkillAction(_context, this));
            _stateDictionary.Add(States.Dash, new Dash(_context, this));
            _stateDictionary.Add(States.KnockBack, new KnockBack(_context, this));
        }

        private static readonly Dictionary<States, State> _stateDictionary = new Dictionary<States, State>();
        
        public State Idle() => GetState(States.Idle);
        public State Move() => GetState(States.Move);
        public State Ground() => GetState(States.Ground);
        public State Air() => GetState(States.Air);
        public State Attack() => GetState(States.Attack);
        public State SkillAction() => GetState(States.SkillAction);
        public State Dash() => GetState(States.Dash);
        public State KnockBack() => GetState(States.KnockBack);

        private State GetState(States stateName)
        {
            if (_stateDictionary.TryGetValue(stateName, out var state)) return state;
            
            return null;
        }
    }

}
