using UnityEngine;
using Script.Core.Audio;

namespace Script.Core
{
    [RequireComponent(typeof(GroundCheck))]
    public class Footstep : MonoBehaviour
    {
        [SerializeField] AudioCueEvent effectEvent;
        [SerializeField] Audio.AudioConfiguration config;
        [SerializeField] FootstepContainer[] container;

        private enum Mode
        {
            Run,
            Jump,
            Land
        }
        private GroundCheck ground;

        private void Awake()
        {
            ground = GetComponent<GroundCheck>();
        }

        // for Animation event call
        public void PlayFootstep()
        {
            GetAudioFromGroundType(ground.Type,Mode.Run);
        }
        public void PlayJumpSound()
        {
            GetAudioFromGroundType(ground.Type, Mode.Jump);
        }
        public void PlayLandSound()
        {
            GetAudioFromGroundType(ground.Type, Mode.Land);
        }

        private void GetAudioFromGroundType(GroundType type,Mode mode)
        {
            foreach (var list in container)
            {
                if(list.Type == type && mode == Mode.Run) SendEvent(list.RunList);
                else if (list.Type == type && mode == Mode.Jump) SendEvent(list.JumpList);
                else if (list.Type == type && mode == Mode.Land) SendEvent(list.LandList);
            }
        }
        private void SendEvent(AudioList audioList)
        {
            if (audioList != null) effectEvent.RiseEvent(audioList.GetRandomAudio(), config, transform.position);
        }
    }
}
