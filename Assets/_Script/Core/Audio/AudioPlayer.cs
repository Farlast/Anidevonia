using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Core.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] AudioCueEvent BGMCueEvent = default;
        [SerializeField] AudioCueEvent VFXCueEvent = default;
        [SerializeField] AudioCueEvent AttackVFXCueEvent = default;
        [SerializeField] AudioSource musicAudioSource;
        [SerializeField] AudioSource effectAudioSource;
        [SerializeField] AudioSource attackEffectAudioSource;

        #region event
        void Start()
        {
            BGMCueEvent.onEventRaised += OnMusicEvent;
            VFXCueEvent.onEventRaised += OnEffectEvent;
            AttackVFXCueEvent.onEventRaised += OnAttackEffectEvent;
        }
        private void OnDestroy()
        {
            BGMCueEvent.onEventRaised -= OnMusicEvent;
            VFXCueEvent.onEventRaised -= OnEffectEvent;
            AttackVFXCueEvent.onEventRaised -= OnAttackEffectEvent;
        }
        #endregion

        #region Main
        private void OnMusicEvent(AudioClip clip,AudioConfiguration configuration,Vector3 pos)
        {
            SettingsAudio(clip, musicAudioSource, configuration);
            musicAudioSource.Play();
        }
        private void OnEffectEvent(AudioClip clip, AudioConfiguration configuration, Vector3 pos)
        {
            SettingsAudio(clip, effectAudioSource, configuration);
            effectAudioSource.PlayOneShot(clip);
        }
        private void OnAttackEffectEvent(AudioClip clip, AudioConfiguration configuration, Vector3 pos)
        {
            SettingsAudio(clip, attackEffectAudioSource, configuration);
            attackEffectAudioSource.PlayOneShot(clip);
        }
        #endregion

        #region Setting
        void SettingsAudio(AudioClip clip, AudioSource source, AudioConfiguration configuration)
        {
            source.outputAudioMixerGroup = configuration.mixerGroupOutput;
            source.clip = clip;
            source.loop = configuration.loop;
            source.mute = configuration.mute;
            source.volume = configuration.volume;
            source.pitch = configuration.pitch;
            source.playOnAwake = configuration.playOnAwake;
            source.panStereo = configuration.panStereo;
            source.reverbZoneMix = configuration.reverbMix;

            source.spatialBlend = configuration.spatialBlend;
            source.maxDistance = configuration.maxDistance;
            source.minDistance = configuration.minDistance;

            source.bypassEffects = configuration.bypassEffect;
            source.bypassReverbZones = configuration.IgnoreReverbZone;

            source.priority = (int)configuration.priority;
        }
        #endregion
    }
}
