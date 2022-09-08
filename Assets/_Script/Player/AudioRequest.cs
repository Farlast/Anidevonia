using Script.Core.Audio;
using UnityEngine;

namespace Script.Player
{
    public class AudioRequest : MonoBehaviour
    {
        [SerializeField] AudioCueEvent effectEvent;
        [SerializeField] Core.Audio.AudioConfiguration effectConfig;
        [SerializeField] AudioList runAudioList;

        public void PlayAudio()
        {
            effectEvent.RiseEvent(runAudioList.GetRandomAudio(), effectConfig, transform.position);
        }
        public void AttackSound()
        {
            effectEvent.RiseEvent(runAudioList.GetRandomAudio(), effectConfig, transform.position);
        }
       
    }
}
