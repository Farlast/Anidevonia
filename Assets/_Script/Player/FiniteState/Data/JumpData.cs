using System;
using UnityEngine;

namespace Script.Player
{
    [Serializable]
    public class JumpData
    {
        public float JumpForce;
        public bool IsGround;
        public float LandingAnimationTime;
    }
}
