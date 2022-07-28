using UnityEngine;
using System;

namespace Script.Core
{
    [Serializable]
    public class PlayerSaveData
    {
        public Vector3 SavePosition;
        public int money;

        public float HP;
        public float MaxHP;
        public int MP;
        public int MaxMP;
       
    }
}
