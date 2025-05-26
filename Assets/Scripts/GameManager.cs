using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;
    public int enemiesKilled = 0;
    public int enemiesToKillForNextLevel = 10;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Sahne geçiþinde kaybolmaz
    }

    public void OnEnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemiesToKillForNextLevel)
        {
            GoToNextLevel();
        }
    }

    void GoToNextLevel()
    {
        enemiesKilled = 0;
        currentLevel++;

        if (currentLevel == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
        }
        else if (currentLevel == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BossScene");
        }
    }
}
