using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UnloadSceneEvent", menuName = "Event/UnloadScene", order = 1)]
public class UnloadSceneEventSO : ScriptableObject
{
    public event UnityAction<GameSceneSO> OnUnload;

    public void RaiseEvent(GameSceneSO value)
    {
        this.OnUnload?.Invoke(value);
    }
}