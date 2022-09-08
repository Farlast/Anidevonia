using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Script.Enemy
{
    public class Transition
    {
        public Func<bool> Decision { get; }
        public IState To { get; }

        public Transition(Func<bool> decision, IState to)
        {
            Decision = decision;
            To = to;
        }
    }
}
