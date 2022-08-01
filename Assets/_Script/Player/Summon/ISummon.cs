using UnityEngine;

namespace Script.Player
{
    public interface ISummon 
    {
        void Summon();
        void ReturnSummon();
    }
    public abstract class Summon : MonoBehaviour
    {
        public abstract void OnSummon(Transform transform);
        public abstract void OnReturnSummon(Transform transform);

        public void MoveToPlayer(Transform transform,Vector2 position,Vector2 traget,float speed)
        {
            Vector3 diraction = Helpers.GetDirection(position, traget);
            transform.position += diraction * speed * Time.deltaTime;
        }
    }
}
