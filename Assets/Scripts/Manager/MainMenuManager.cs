using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private LoadSceneEventSO loadSceneEventSO;

    [SerializeField] private QuitGameEventSO quitGameEventSO;

    [SerializeField] private GameSceneSO hubSceneSO;

    public void Play()
    {
        if (this.loadSceneEventSO != null)
        {
            this.loadSceneEventSO.RaiseEvent(hubSceneSO);
        }
    }

    public void Quit()
    {
        if (this.quitGameEventSO != null)
        {
            this.quitGameEventSO.RaiseEvent();
        }
    }
}