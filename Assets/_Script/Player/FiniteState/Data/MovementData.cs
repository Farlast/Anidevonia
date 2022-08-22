using System;
using UnityEngine;

namespace Script.Player
{
    [Serializable]
    public class MovementData
    {
        public Vector2 MoveInput;
        public float MoveSpeed;
        public float SpeedMultiply;
        public float MaxVelocity;
        public float MaxFallVelocity;
        public float DecelerationSpeed;
        public float MinMoveVelocity;
        public bool IsWallAtFront;
    }
}
