using UnityEngine;

namespace Script.Player
{
    public interface IState
    {
        void Enter();
        void Exit();
        void HandleInput();
        void Update();
        void FixUpdate();
        void OnAnimationEventEnter();
        void OnAnimationEventExit();
        void OnAnimationTransitionEvent();
    }
}
