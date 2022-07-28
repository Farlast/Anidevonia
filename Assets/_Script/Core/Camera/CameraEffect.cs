using UnityEngine;
using Cinemachine;

namespace Script.Core.Camera
{
    public class CameraEffect : MonoBehaviour
    {
        [field:SerializeField] public CinemachineVirtualCamera _activeVirtualCamera { get; private set;}
        //private CinemachineFramingTransposer _cinemachineFramingTransposer;
        private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
        private float _shakeLength = 0;
        
        void Update()
        {
            if (_multiChannelPerlin == null) return;
            if (_shakeLength > 0)
            {
                _shakeLength -= Time.deltaTime;
                if (_shakeLength <= 0f)
                {
                    _multiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }
        public void SetCamera(CinemachineVirtualCamera cinemachine)
        {
            if (cinemachine == null) return;
            _activeVirtualCamera = cinemachine;
            //_cinemachineFramingTransposer = _activeVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _multiChannelPerlin = _activeVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        public void Shake(float shakeAmplitude, float frequency, float length)
        {
            _shakeLength = length;
            _multiChannelPerlin.m_FrequencyGain = frequency;
            _multiChannelPerlin.m_AmplitudeGain = shakeAmplitude;
        }
    }
}
