using UnityEngine;
using UnityEngine.Events;

namespace Script.Core.Audio
{
    [CreateAssetMenu(menuName = "ScriptableObject/Audio/Audio Cue")]
    public class AudioCueEvent : ScriptableObject
    {
        public UnityAction<AudioClip,AudioConfiguration, Vector3> onEventRaised;

        public void RiseEvent(AudioClip clip,AudioConfiguration config, Vector3 position)
        {
            onEventRaised?.Invoke(clip, config, position);
        }
    }
}
