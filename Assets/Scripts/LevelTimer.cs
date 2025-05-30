using UnityEngine;
using TMPro;
using System.Collections;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float remainingTime;
    private bool timerStarted = false;

    void Start()
    {
        StartCoroutine(WaitForValidTime());
    }

    IEnumerator WaitForValidTime()
    {
        // 🔄 Bekle ki GameManager.levelTime doğru dolmuş olsun
        while (GameManager.Instance.levelTime <= 0f)
        {
            yield return null; // 1 frame bekle
        }

        remainingTime = GameManager.Instance.levelTime;
        timerStarted = true;
        UpdateTimerUI();

        Debug.Log($"[LevelTimer] Başlatıldı. Süre: {remainingTime}");
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
