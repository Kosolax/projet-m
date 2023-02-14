using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class RebindInputUI : MonoBehaviour
{
    [SerializeField] private InputEventSO inputEventSO;

    [SerializeField] private GameObject keyRebindPrefab;

    [SerializeField] private Transform parentToInstantiateKeyRebind;

    [SerializeField] private GameObject rebindingPanel;

    [SerializeField] private TextMeshProUGUI rebindingText;

    private void OnEnable()
    {
        if (this.inputEventSO != null)
        {
            this.RegisterEvents();
            this.GenerateKeyRebindInAGenericWay(true);
        }
    }

    private void OnDisable()
    {
        if (this.inputEventSO != null)
        {
            this.UnRegisterEvents();
        }
    }

    private void RegisterEvents()
    {
        this.inputEventSO.OnRebindFinished += () => HideRebindUI();
        this.inputEventSO.OnRebindStarted += () => ShowRebindUI();
    }

    private void UnRegisterEvents()
    {
        this.inputEventSO.OnRebindFinished += () => HideRebindUI();
        this.inputEventSO.OnRebindStarted += () => ShowRebindUI();
    }

    private void ShowRebindUI()
    {
        this.rebindingPanel.SetActive(true);
        this.rebindingText.text = "Press a key";
    }

    private void HideRebindUI()
    {
        this.rebindingPanel.SetActive(false);
        this.rebindingText.text = string.Empty;
        this.ResetGeneratedKeyRebindUI();
        this.GenerateKeyRebindInAGenericWay(false);
    }

    private void ResetGeneratedKeyRebindUI()
    {
        foreach (Transform child in this.parentToInstantiateKeyRebind.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateKeyRebindInAGenericWay(bool shouldLoadOverridedValues)
    {
        IEnumerator<InputAction> enumerator = this.inputEventSO.GetDungeonInputEnumerator();

        List<KeyRebind> keyRebinds = new List<KeyRebind>();

        while (enumerator.MoveNext())
        {
            ReadOnlyArray<InputBinding> bindings = enumerator.Current.bindings;

            Dictionary<int, KeyRebind> gamepadDictionary = new Dictionary<int, KeyRebind>();
            Dictionary<int, KeyRebind> keyBoardDictionary = new Dictionary<int, KeyRebind>();

            int localGamepadIndex = 0;
            int localKeyboardIndex = 0;

            for (int i = 0; i < bindings.Count; i++)
            {
                InputBinding binding = bindings[i];

                if (shouldLoadOverridedValues)
                {
                    this.inputEventSO.LoadBindingOverride(binding.action);
                }

                if (binding.isPartOfComposite || (!binding.isPartOfComposite && !binding.isComposite))
                {
                    if (IsController(binding.path))
                    {
                        gamepadDictionary.Add(localGamepadIndex, new KeyRebind()
                        {
                            Action = binding.action,
                            ActionName = binding.name != null ? binding.name : binding.action,
                            GamepadInput = binding.ToDisplayString(),
                            GamepadIndex = i,
                        });
                        localGamepadIndex++;
                    }
                    else if (IsKeyBoard(binding.path))
                    {
                        keyBoardDictionary.Add(localKeyboardIndex, new KeyRebind()
                        {
                            ActionName = binding.name != null ? binding.name : binding.action,
                            KeyboardInput = enumerator.Current.GetBindingDisplayString(i),
                            KeyboardIndex = i,
                        });
                        localKeyboardIndex++;
                    }
                }
            }

            foreach (var keyValuePair in gamepadDictionary)
            {
                keyValuePair.Value.KeyboardInput = keyBoardDictionary[keyValuePair.Key].KeyboardInput;
                keyValuePair.Value.KeyboardIndex = keyBoardDictionary[keyValuePair.Key].KeyboardIndex;
                keyRebinds.Add(keyValuePair.Value);
            }
        }

        foreach (var keyRebind in keyRebinds)
        {
            GameObject instance = Object.Instantiate(this.keyRebindPrefab, this.parentToInstantiateKeyRebind);
            KeyRebindUI keyRebindUI = instance.GetComponent<KeyRebindUI>();
            keyRebindUI.SetAction(keyRebind.Action);
            keyRebindUI.SetActionNameText(keyRebind.ActionName);
            keyRebindUI.SetKeyboardInputText(keyRebind.KeyboardInput);
            keyRebindUI.SetGamePadInputText(keyRebind.GamepadInput);
            keyRebindUI.SetGamepadIndex(keyRebind.GamepadIndex);
            keyRebindUI.SetKeyboardIndex(keyRebind.KeyboardIndex);
        }
    }

    private bool IsController(string path)
    {
        return path.Split("/")[0] == "<Gamepad>";
    }

    private bool IsKeyBoard(string path)
    {
        return path.Split("/")[0] == "<Keyboard>";
    }
}