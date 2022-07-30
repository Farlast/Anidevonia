using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Event/Transform event channel")]
public class TransformEventChannel : ScriptableObject
{
    public UnityAction<Transform> onEventRaised;

    public void RiseEvent(Transform transform)
    {
        onEventRaised?.Invoke(transform);
    }
}
