using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class KeyRebindUI : MonoBehaviour
{
    [SerializeField] private InputEventSO inputEventSO;

    [SerializeField] private TextMeshProUGUI actionNameText;

    [SerializeField] private TextMeshProUGUI keyboardInputText;

    [SerializeField] private TextMeshProUGUI gamePadInputText;

    [SerializeField] private InputAction currentInputAction;

    [SerializeField] private int keyboardIndex;

    [SerializeField] private int gamepadIndex;

    public void SetKeyboardIndex(int index)
    {
        this.keyboardIndex = index;
    }

    public void SetGamepadIndex(int index)
    {
        this.gamepadIndex = index;
    }
    
    public void SetAction(string action)
    {
        if (this.inputEventSO != null)
        {
            this.currentInputAction = this.inputEventSO.FindAction(action);
        }
    }

    public void SetActionNameText(string text)
    {
        if (this.actionNameText != null)
        {
            this.actionNameText.text = text;
        }
    }
    
    public void SetKeyboardInputText(string text)
    {
        this.keyboardInputText.text = text;
    }
    
    public void SetGamePadInputText(string text)
    {
        this.gamePadInputText.text = text;
    }

    public void RebindKeyboard()
    {
        if (this.inputEventSO != null)
        {
            this.inputEventSO.RebindKey(this.currentInputAction, this.keyboardIndex);
        }
    }

    public void RebindGamepad()
    {
        if (this.inputEventSO != null)
        {
            this.inputEventSO.RebindKey(this.currentInputAction, this.gamepadIndex);
        }
    }

    public void ResetBinding()
    {
        if (this.inputEventSO != null)
        {
            this.inputEventSO.ResetBinding(this.currentInputAction, this.gamepadIndex);
            this.inputEventSO.ResetBinding(this.currentInputAction, this.keyboardIndex);
        }
    }
}