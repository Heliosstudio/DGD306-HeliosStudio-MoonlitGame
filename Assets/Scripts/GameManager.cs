using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float levelTime;           // O andan bir önceki sahneden gelen, ya da SetTimeForScene ile ayarlanacak
    public string currentLevel;       // Örneğin "Scene1", "Scene2"…
    public int currentLevelIndex;

    // ► Yeni eklenen kısım: Kaç oyuncu hayatta?
    private int playersAlive = 2;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Sahne yüklendiğinde OnSceneLoaded tetiklenir
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
            case "Scene1": levelTime = 90f; break;
            case "Scene2": levelTime = 120f; break;
            case "Scene3": levelTime = 150f; break;
            default: levelTime = 0f; break;
        }

        // Oyun sahnesindeysek hayatta oyuncu sayısını sıfırla
        if (scene.name == "Scene1" || scene.name == "Scene2" || scene.name == "Scene3")
        {
            playersAlive = 2;
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

    // ► Yeni eklenen metot: Bir oyuncu öldüğünde çağrılır
    public void OnPlayerDied()
    {
        playersAlive--;
        Debug.Log($"[GameManager] Bir oyuncu öldü. Kalan oyuncu sayısı = {playersAlive}");
        if (playersAlive <= 0)
        {
            // İki oyuncu da öldü → Ana menüye dön
            SceneManager.LoadScene("MainMenu");
        }
    }
}
