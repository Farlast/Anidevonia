using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Core.Audio
{
    [CreateAssetMenu(menuName = "ScriptableObject/Audio/Audio list")]
    public class AudioList : ScriptableObject
    {
        [SerializeField] List<AudioClip> audios = new();

        public AudioClip GetRandomAudio()
        {
            int index = Random.Range(0,audios.Count);
            return audios[index];
        }
    }
}
