using System.Collections;
using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class PlayerSummonSystem : MonoBehaviour
    {
        [SerializeField] Summon summon;
        [SerializeField] TransformEventChannel summonEvent;
        [SerializeField] Transform playerTransfrom;
        //[SerializeField] bool IsSummonActive;

        private void Start()
        {
            //IsSummonActive = false;

            summon.gameObject.SetActive(false);

            summonEvent.onEventRaised += GetSummon;
        }
        private void OnDestroy()
        {
            summonEvent.onEventRaised -= GetSummon;
        }
        public void GetSummon(Transform transform)
        {
            playerTransfrom = transform;
            summon.OnSummon(transform);
        }

    }
}
