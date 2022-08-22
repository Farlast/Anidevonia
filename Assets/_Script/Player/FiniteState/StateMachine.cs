using UnityEngine;

namespace Script.Player
{
    public abstract class StateMachine
    {
        protected IState CurrentState;

        public void ChangeState(IState newstate)
        {
            if (CurrentState == newstate) return;
            if (CurrentState != null) CurrentState.Exit();

            CurrentState = newstate;

            CurrentState.Enter();
        }
        public void HandleInput()
        {
            CurrentState?.HandleInput();
        }
        public void Update()
        {
            CurrentState?.Update();
        }
        public void FixUpdate()
        {
            CurrentState?.FixUpdate();
        }
        public void OnAnimationEnterEvent()
        {
            CurrentState?.OnAnimationEventEnter();
        }
        public void OnAniMationEnterEventExit()
        {
            CurrentState?.OnAnimationEventExit();
        }
        public void OnAniMationEnterEventTransition()
        {
            CurrentState?.OnAnimationTransitionEvent();
        }
    }
}
