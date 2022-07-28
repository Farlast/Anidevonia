using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] string Name;
        [SerializeField] Sprite Image;
        [TextArea(3,10)][SerializeField] string description;
        [SerializeField] Type itemType;
        [SerializeField] GameObject prefab;

        public enum Type
        {
            none,
            upgrade,
            money
        }
    }
}
