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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

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
        GameManager.Instance.levelTime = remainingTime; 

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            HandleTimeout();  
        }

        UpdateTimerUI();
    }

    void HandleTimeout()
    {

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Scene3")
        {

            SceneManager.LoadScene("MainMenu");
        }
        else
        {

            GameManager.Instance.GoToNextLevel();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
