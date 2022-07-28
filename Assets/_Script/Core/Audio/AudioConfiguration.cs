using UnityEngine;
using UnityEngine.Audio;

namespace Script.Core.Audio
{
    [CreateAssetMenu(menuName = "ScriptableObject/Audio/Audio Configuration")]
    public class AudioConfiguration : ScriptableObject
    {
        public enum Priority
        {
            Standard,
            Important
        }
        public AudioMixerGroup mixerGroupOutput;
        public Priority priority;

        [Space]
        [Header("Sound properties")]
        public bool loop;
        public bool mute;
        public bool playOnAwake;

        [Range(0, 1)]
        public float volume;

        [Range(-3, 3)]
        public float pitch;

        [Range(-1, 1)][Tooltip("Sound in 3D space")]
        public float panStereo;

        [Range(0, 1.5f)]
        public float reverbMix;

        [Space]
        [Header("spatialisation")]

        [Range(0, 1)]
        public float spatialBlend;

        [Range(0, 100)]
        public float minDistance;

        [Range(0, 100)]
        public float maxDistance;

        [Space]
        [Header("Ignores")]
        public bool bypassEffect;
        public bool IgnoreReverbZone;
    }
}
