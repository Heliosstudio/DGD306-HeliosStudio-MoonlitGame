using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float remainingTime;
    private bool timerStarted = false;

    void OnEnable()
    {
        // Sahne yüklendiğinde Timer'ı ayağa kaldıracak metodu ekle
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Scene yüklendiğinde tetiklenen metod
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // GameManager'in levelTime'ı burada zaten set edilmiş olacak
        remainingTime = GameManager.Instance.levelTime;
        timerStarted = true;
        UpdateTimerUI();

        Debug.Log($"[LevelTimer] Sahne yüklendi: {scene.name}. Kalan süre: {remainingTime}");
    }

    void Update()
    {
        if (!timerStarted || remainingTime <= 0f)
            return;

        remainingTime -= Time.deltaTime;
        GameManager.Instance.levelTime = remainingTime; // istersen kaydedebilirsin

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            GameManager.Instance.GoToNextLevel();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
