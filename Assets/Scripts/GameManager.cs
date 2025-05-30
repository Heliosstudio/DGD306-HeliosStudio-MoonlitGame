using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("1-based level index; starts at 1 for Scene1")]
    public int currentLevel = 1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Timer dolunca çaðrýlacak.
    /// </summary>
    public void GoToNextLevel()
    {
        currentLevel++;
        string nextScene = "";

        switch (currentLevel)
        {
            case 1:
                nextScene = "Scene1"; break;
            case 2:
                nextScene = "Scene2"; break;
            case 3:
                nextScene = "Scene3"; break;
            default:
                Debug.LogWarning($"No scene mapped for level {currentLevel}, restarting.");
                RestartLevel();
                return;
        }

        SceneManager.LoadScene(nextScene);
    }


    public void RestartLevel()
    {
        string sceneName = $"Scene{currentLevel}";
        SceneManager.LoadScene(sceneName);
    }
}
