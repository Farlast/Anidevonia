using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Core.Audio
{
    public class AudioZone : MonoBehaviour
    {
        [SerializeField] AudioList AudioList;
        [SerializeField] AudioConfiguration configuration;
        [SerializeField] AudioCueEvent BGMCueEvent = default;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var clip = AudioList.GetRandomAudio();
            BGMCueEvent?.RiseEvent(clip, configuration, transform.position);
        }
    }
}
