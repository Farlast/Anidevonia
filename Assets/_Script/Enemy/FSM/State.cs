using UnityEngine;

namespace Script.Enemy
{
    public abstract class State : MonoBehaviour
    {
        protected StateController controller;
        private void Awake()
        {
            controller = GetComponent<StateController>();
        }
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchState();
        public void SwicthState(State newState)
        {
            ExitState();
            newState.EnterState();
            controller.CurrentState = newState;
        }
      
    }
}
