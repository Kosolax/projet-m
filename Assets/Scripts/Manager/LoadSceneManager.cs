using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private LoadSceneEventSO loadSceneEventSO;

    [SerializeField] private UnloadSceneEventSO unloadSceneEventSO;

    private void LoadScene(GameSceneSO gameScene)
    {
        if (gameScene != null)
        {
            SceneManager.LoadScene(gameScene.Name, gameScene.LoadingMode);
        }
    }

    private void UnloadScene(GameSceneSO gameScene)
    {
        if (gameScene != null)
        {
            SceneManager.UnloadSceneAsync(gameScene.Name);
        }
    }

    private void OnEnable()
    {
        if (this.loadSceneEventSO != null)
        {
            this.loadSceneEventSO.OnLoad += (gameScene) => LoadScene(gameScene);
        }

        if (this.unloadSceneEventSO != null)
        {
            this.unloadSceneEventSO.OnUnload += (gameScene) => UnloadScene(gameScene);
        }
    }

    private void OnDisable()
    {
        if (this.loadSceneEventSO != null)
        {
            this.loadSceneEventSO.OnLoad -= (gameScene) => LoadScene(gameScene);
        }

        if (this.unloadSceneEventSO != null)
        {
            this.unloadSceneEventSO.OnUnload -= (gameScene) => UnloadScene(gameScene);
        }
    }
}