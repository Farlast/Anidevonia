using Script.Core.Audio;
using UnityEngine;

namespace Script.Player
{
    public class AudioRequest : MonoBehaviour
    {
        [SerializeField] AudioCueEvent effectEvent;
        [SerializeField] Core.Audio.AudioConfiguration effectConfig;

        [SerializeField] AudioList audiolist;

        public void AudioRun()
        {
            effectEvent.RiseEvent(audiolist.GetRandomAudio(), effectConfig, transform.position);
        }
    }
}
