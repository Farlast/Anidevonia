using UnityEngine;

namespace Script.Core
{
    public enum GroundType
    {
        None,
        Grass,
        TallGrass,
        Gravel,
        Rock,
        Metal,
        Wood
    }
    public class Ground : MonoBehaviour
    {
        [field:SerializeField] public GroundType Type { get; private set; }
    }
}
