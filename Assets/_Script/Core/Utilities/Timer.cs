using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Script.Core
{
    public class Timer : MonoBehaviour
    {
        public void SetTime(Action action, float time)
        {
            StartCoroutine(DelayAction(action, time));
        }

        IEnumerator DelayAction(Action action,float time)
        {
            yield return Helpers.GetWait(time);
            action();
        }
    }
}
