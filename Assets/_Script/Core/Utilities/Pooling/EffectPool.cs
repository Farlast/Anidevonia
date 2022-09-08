using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Core
{
    public class EffectPool : MonoBehaviour
    {
        [SerializeField] private GameObject getHitEffect;
        private static ObjectPool<GameObject> effect;
        private static Transform Parent;
        private void Start()
        {
            Parent = this.transform;
            effect = new ObjectPool<GameObject>(
                ()=> { return Instantiate(getHitEffect); },
                shape => { shape.SetActive(true); },
                shape => { shape.SetActive(false); },
                shape => {Destroy(shape);},
                false,
                10,
                20
            );
        }
        public static GameObject GetEffect(Transform target)
        {
            GameObject obj = effect.Get();
            obj.transform.SetParent(Parent);
            obj.transform.position = target.position;
            return obj;
        }
        public static void ReturnEffect(GameObject effectObj)
        {
            effect.Release(effectObj);
        }
       
        public static GameObject GetAndAutoReturnToPool(Transform target, float time)
        {
            var returnEffect = GetEffect(target);
            TimerSystem.Create(() => { ReturnEffect(returnEffect); }, time);
            return returnEffect;
        }
    }
}
