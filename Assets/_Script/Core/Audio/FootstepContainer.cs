using UnityEngine;
using Script.Core.Audio;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/Audio/FootstepContainer")]
    public class FootstepContainer : ScriptableObject
    {
        [field: SerializeField] public GroundType Type { get; private set; }
        [field: SerializeField] public AudioList RunList { get; private set; }
        [field: SerializeField] public AudioList JumpList { get; private set; }
        [field: SerializeField] public AudioList LandList { get; private set; }
    }
}
