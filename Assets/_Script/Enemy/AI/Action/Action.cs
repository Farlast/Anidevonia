using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{

    public abstract class Action : IState
    {
        protected EnemyBase Agent;
        protected StateMachine StateMachine;
        protected Action(EnemyBase enemy , StateMachine stateMachine)
        {
            Agent = enemy;
            StateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
            //Debug.Log("enter : " + GetType().Name);
        }

        public virtual void OnExit()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixUpdate()
        {
        }
    }
}
