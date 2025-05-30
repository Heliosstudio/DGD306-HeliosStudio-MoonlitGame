using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float levelTime;           // Her levelin süresi (LevelTimer kullanır)
    public string currentLevel;       // Aktif sahnenin adı
    public int currentLevelIndex;     // Aktif sahnenin index'i

    void Awake()
    {
        Debug.Log($"[GameManager] Awake çalıştı: {this.GetHashCode()}");

        if (Instance != null && Instance != this)
        {
            Debug.Log("İkinci bir GameManager bulundu, silindi.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Sahne yüklendiğinde tetiklenir
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevel = scene.name;
        currentLevelIndex = scene.buildIndex;

        SetTimeForScene(currentLevel); // Süreyi ayarla
        Debug.Log($"[GameManager] {currentLevel} sahnesi yüklendi. Süre: {levelTime} saniye");
    }

    // Sahne adına göre süre belirle
    public void SetTimeForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Scene1":
                levelTime = 10f;
                break;
            case "Scene2":
                levelTime = 12f;
                break;
            case "Scene3":
                levelTime = 15f;
                break;
            default:
                levelTime = 60f;
                break;
        }
    }

    // Bir sonraki level'a geç
    public void GoToNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("Oyun bitti!");
            // Buraya WinScene yükleme veya ana menüye dönüş eklenebilir
        }
    }
}
