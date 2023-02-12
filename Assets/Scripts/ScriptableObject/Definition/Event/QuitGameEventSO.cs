using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "QuitGameEvent", menuName = "Event/QuitGame", order = 1)]
public class QuitGameEventSO : ScriptableObject
{
    public event UnityAction OnQuit;

    public void RaiseEvent()
    {
        this.OnQuit?.Invoke();
    }
}