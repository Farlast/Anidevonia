using UnityEngine;

namespace Script.Enemy
{
    public class SeeTarget : IDecision
    {
        public override bool Decide(Enemy enemy)
        {
            return enemy.detector.SeeTarget;
        }
    }
}
