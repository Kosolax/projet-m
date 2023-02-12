using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameScene", menuName = "GameScene", order = 1)]
public class GameSceneSO : ScriptableObject
{
    public string Name;

    public LoadSceneMode LoadingMode;

    public bool NeedLoadingScreen;
}