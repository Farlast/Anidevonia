using Script.Core.Audio;
using UnityEngine;

namespace Script.Player
{
    public class AudioRequest : MonoBehaviour
    {
        [SerializeField] AudioCueEvent effectEvent;
        [SerializeField] Core.Audio.AudioConfiguration effectConfig;
        [SerializeField] AudioList runAudioList;

        [SerializeField] AudioCueEvent attackAudioEvent;
        [SerializeField] Core.Audio.AudioConfiguration attackAudioConfig;
        [SerializeField] AudioList attackAudioList;

        public void AudioRun()
        {
            effectEvent.RiseEvent(runAudioList.GetRandomAudio(), effectConfig, transform.position);
        }
        public void AttackSound()
        {
            attackAudioEvent.RiseEvent(attackAudioList.GetRandomAudio(), attackAudioConfig, transform.position);
        }
    }
}
