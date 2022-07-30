using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class PlayerSummonPool : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] TransformEventChannel summonEvent;
        [SerializeField] VoidEventChannel releaseSummonEvent;
        private void Start()
        {
            prefab.SetActive(false);
            summonEvent.onEventRaised += Get;
            releaseSummonEvent.onEventRaised += Release;
        }

        public void Get(Transform transform)
        {
            prefab.GetComponent<ISummon>().Summon();
            prefab.transform.position = transform.position;
            prefab.SetActive(true);
        }
       public void Release()
        {
            prefab.GetComponent<ISummon>().RemoveSummon();
            prefab.transform.position = Vector2.zero;
            prefab.SetActive(false);
        }
    }
}
