using UnityEngine;

namespace Script.Core
{
    public enum Faceing
    {
        Left,
        Right
    }
    public interface ISprite 
    {
        public void FlipSprite();
    }
}
