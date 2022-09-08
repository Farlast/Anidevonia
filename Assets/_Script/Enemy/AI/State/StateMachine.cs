using UnityEngine;
using System;
using System.Collections.Generic;

namespace Script.Enemy
{
    public class StateMachine
    {
        private IState CurrentState { get; set; }
        private Dictionary <Type,List<Transition>> transitions = new();
        private List<Transition> currentTransitions = new();
        private List<Transition> anyTransitions = new();
        private static readonly List<Transition> EmptyTransitions = new();
        public bool StopUpdateTransition { get; set; } = false;
        public string GetCurrentState()
        {
            return CurrentState.GetType().Name;
        }
        public void Update()
        {
            var transition = GetTransition();
            if(transition != null)
            {
                SetState(transition.To);
            }
            CurrentState?.Update();
        }
        public void FixUpdate()
        {
            CurrentState?.FixUpdate();
        }
        public void SetState(IState state)
        {
            if (state == CurrentState) return;
            CurrentState?.OnExit();
            CurrentState = state;

            transitions.TryGetValue(CurrentState.GetType(), out currentTransitions);
            if (currentTransitions == null) currentTransitions = EmptyTransitions;
            CurrentState?.OnEnter();
        }
        public void AddTransition(IState from, IState to, Func<bool> decision)
        {
            if(transitions.TryGetValue(from.GetType(), out var transition))
            {
                transition.Add(new Transition(decision, to));
                transitions[from.GetType()] = transition;
            }
            else
            {
                transition = new(){new Transition(decision, to)};
                transitions[from.GetType()] = transition;
            }
        }
        public void AddAnyTransition(IState state, Func<bool> decision)
        {
            anyTransitions.Add(new Transition(decision, state));
        }
        private Transition GetTransition()
        {
            if (StopUpdateTransition) return null;

            foreach (var transition in anyTransitions)
            {
                if (transition.Decision()) return transition;
            }
            foreach (var transition in currentTransitions)
            {
                if (transition.Decision()) return transition;
            }
            return null;
        }
    }
}
