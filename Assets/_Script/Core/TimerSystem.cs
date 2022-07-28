using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Script.Core
{
    public class TimerSystem : MonoBehaviour
    {
        public enum TimerMode
        {
            Refill,
            StackUp
        }
        static Action OnUpdate;

        private static Dictionary<string, ActionTimer> activeTimerList = new();
        public static ActionTimer Create(Action action, float time, string id, TimerMode mode = TimerMode.Refill)
        {
            if (activeTimerList.ContainsKey(id))
            {
                switch (mode)
                {
                    case TimerMode.Refill:
                        RefillTime(time, id);
                        break;
                    case TimerMode.StackUp:
                        StackTime(time, id);
                        break;
                }
                return activeTimerList[id];
            }

            ActionTimer timer = new ActionTimer(action, time, id);
            OnUpdate += timer.Update;
            activeTimerList.Add(id, timer);
            return timer;

        }
        public static ActionTimer Create(Action action, float time)
        {
            ActionTimer timer = new ActionTimer(action, time);
            OnUpdate += timer.Update;
            return timer;
        }
        public static void RefillTime(float time, string id)
        {
            activeTimerList[id].RefillTime(time);
        }
        public static void StackTime(float time, string id)
        {
            activeTimerList[id].AddTime(time);
        }
        public static void Delete(ActionTimer timer, string id)
        {
            OnUpdate -= timer.Update;
            if (string.IsNullOrEmpty(id)) return;
            activeTimerList.Remove(id);
        }
       
        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
    public class ActionTimer
    {
        private string id;
        private Action action;
        private float time;
        private bool isEnd;
        public ActionTimer(Action action, float time)
        {
            this.action = action;
            this.time = time;
        }
        public ActionTimer(Action action, float time, string id)
        {
            this.action = action;
            this.time = time;
            this.id = id;
        }
        public float GetTime()
        {
            return time;
        }
        public void AddTime(float extraTime)
        {
            time += extraTime;
        }
        public void RefillTime(float extraTime)
        {
            time = extraTime;
        }
        public void Update()
        {
            if (isEnd) return;

            time -= Time.deltaTime;
            if (time < 0)
            {
                action();
                DestroySelf();
            }
        }
        private void DestroySelf()
        {
            isEnd = true;
            TimerSystem.Delete(this,id);
        }
    }
}
