using System;
using UnityEngine;

namespace Script.Player
{
    [Serializable]
    public class KnockBackData
    {
        [field: SerializeField] public float StuntTime { get; private set; }
        [field: SerializeField] public float KnockBackForceLow { get; private set; }
        [field: SerializeField] public float KnockBackForceHeavy { get; private set; }
    }
}
