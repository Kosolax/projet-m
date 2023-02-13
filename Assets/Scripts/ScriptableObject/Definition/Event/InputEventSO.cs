using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using static DungeonInput;

[CreateAssetMenu(fileName = "InputEvent", menuName = "Event/Input", order = 1)]
public class InputEventSO : ScriptableObject, IGamePlayActions
{
    public event UnityAction JumpEvent;

    public event UnityAction<Vector2> MoveEvent;

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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (this.JumpEvent != null && context.phase == InputActionPhase.Performed)
        {
            this.JumpEvent.Invoke();
        }
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