using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using static DungeonInput;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

[CreateAssetMenu(fileName = "InputEvent", menuName = "Event/Input", order = 1)]
public class InputEventSO : ScriptableObject, IGamePlayActions
{
    public event UnityAction JumpEvent;

    public event UnityAction<Vector2> MoveEvent;

    public event UnityAction OnRebindStarted;

    public event UnityAction OnRebindFinished;

    private DungeonInput dungeonInput;

    private void OnEnable()
    {
        if (this.dungeonInput == null)
        {
            this.dungeonInput = new DungeonInput();
            this.dungeonInput.GamePlay.SetCallbacks(this);
        }
    }

    private void OnDisable()
    {
        this.DisableAllInput();
    }

    private void DisableAllInput()
    {
        this.DisableGamePlayInput();
    }

    public void RaiseRebindFinishedEvent()
    {
        if (this.OnRebindFinished != null)
        {
            this.OnRebindFinished.Invoke();
        }
    }

    public void RaiseRebindStartedEvent()
    {
        if (this.OnRebindStarted != null)
        {
            this.OnRebindStarted.Invoke();
        }
    }

    public void RebindKey(InputAction actionToRebind, int bindingIndex)
    {
        this.RaiseRebindStartedEvent();

        actionToRebind.Disable();
        
        RebindingOperation rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();
            this.SaveBindingOverride(actionToRebind, bindingIndex);
            this.RaiseRebindFinishedEvent();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();
            this.RaiseRebindFinishedEvent();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");
        rebind.Start();
    }

    private void SaveBindingOverride(InputAction action, int bindingIndex)
    {
        PlayerPrefs.SetString(action.actionMap + action.name + bindingIndex, action.bindings[bindingIndex].overridePath);
    }

    public void LoadBindingOverride(string actionName)
    {
        InputAction action = this.FindAction(actionName);

        if (action != null)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                {
                    action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
                }
            }
        }
    }

    public void ResetBinding(InputAction action, int bindingIndex)
    {
        if (action != null)
        {
            action.RemoveBindingOverride(bindingIndex);
            this.SaveBindingOverride(action, bindingIndex);
            this.RaiseRebindFinishedEvent();
        }
    }

    public InputAction FindAction(string actionName)
    {
        if (this.dungeonInput != null)
        {
            return this.dungeonInput.asset.FindAction(actionName);
        }

        return null;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (this.JumpEvent != null && context.phase == InputActionPhase.Performed)
        {
            this.JumpEvent.Invoke();
        }
    }

    public IEnumerator<InputAction> GetDungeonInputEnumerator()
    {
        return this.dungeonInput.GetEnumerator();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (this.MoveEvent != null)
        {
            this.MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void EnableGamePlayInput()
    {
        this.dungeonInput.GamePlay.Enable();
    }

    public void DisableGamePlayInput()
    {
        this.dungeonInput.GamePlay.Disable();
    }
}