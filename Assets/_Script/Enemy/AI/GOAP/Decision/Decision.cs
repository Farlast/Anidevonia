using UnityEngine;

namespace Script.Enemy
{
    public abstract class IDecision : ScriptableObject
    {
       public abstract bool Decide(Enemy enemy);
    }
}
