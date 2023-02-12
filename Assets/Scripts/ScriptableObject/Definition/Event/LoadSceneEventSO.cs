using System;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LoadSceneEvent", menuName = "Event/LoadScene", order = 1)]
public class LoadSceneEventSO : ScriptableObject
{
    public event UnityAction<GameSceneSO> OnLoad;

    [SerializeField] private UnloadSceneEventSO unloadSceneEventSO;

    private GameSceneSO oldSceneInitialValueSO = null;

    [NonSerialized]
    private GameSceneSO oldSceneSO;

    public void OnAfterDeserialize()
    {
        this.oldSceneSO = this.oldSceneInitialValueSO;
    }

    public void RaiseEvent(GameSceneSO value)
    {
        if (this.oldSceneSO != null)
        {
            this.unloadSceneEventSO.RaiseEvent(this.oldSceneSO);
        }

        this.oldSceneSO = value;
        this.OnLoad?.Invoke(value);
    }
}