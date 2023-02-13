using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LoadSceneEventSO loadSceneEventSO;
    
    [SerializeField] private QuitGameEventSO quitGameEventSO;

    [SerializeField] private GameSceneSO hubSceneSO;

    [SerializeField] private InputEventSO inputEventSO;

    private void Start()
    {
        if (this.loadSceneEventSO != null)
        {
            this.loadSceneEventSO.RaiseEvent(this.hubSceneSO);
        }

        if (this.inputEventSO != null)
        {
            // TODO
            this.inputEventSO.EnableGamePlayInput();
        }
    }

    private void OnEnable()
    {
        if (this.quitGameEventSO != null)
        {
            this.quitGameEventSO.OnQuit += () => QuitGame();
        }
    }

    private void OnDisable()
    {
        if (this.quitGameEventSO != null)
        {
            this.quitGameEventSO.OnQuit -= () => QuitGame();
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}