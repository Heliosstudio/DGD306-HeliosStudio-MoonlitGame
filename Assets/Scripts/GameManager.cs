using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float levelTime;           // O andan bir önceki sahneden gelen, ya da SetTimeForScene ile ayarlanacak
    public string currentLevel;       // Örneğin "Scene1", "Scene2"…
    public int currentLevelIndex;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // İlk açılışta (örneğin MainMenu’den start’a basılırken) 
        // eğer doğrudan Scene1 yükleniyorsa, levelTime ayarlansın:
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevel = scene.name;
        currentLevelIndex = scene.buildIndex;

        // Sahnenin adına göre levelTime ataması:
        switch (scene.name)
        {
            case "Scene1": levelTime = 4f; break;
            case "Scene2": levelTime = 4f; break;
            case "Scene3": levelTime = 50f; break;
            default: levelTime = 0f; break;
        }

        Debug.Log($"[GameManager] OnSceneLoaded → {scene.name}, levelTime = {levelTime}");
    }

    public void GoToNextLevel()
    {
        int nextIndex = currentLevelIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("[GameManager] Son level tamamlandı.");
        }
    }
}
