using UnityEngine;

namespace Script.Enemy
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
        void Update();
        void FixUpdate();
    }
}
