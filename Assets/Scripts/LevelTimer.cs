using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float levelDuration = 60f;          // Level1 s�resi (saniye)
    private float remainingTime;

    [Header("UI")]
    public TextMeshProUGUI timerText;          // Inspector�dan atayaca��z

    void Start()
    {
        remainingTime = levelDuration;
        UpdateTimerUI();
    }

    void Update()
    {
        if (remainingTime <= 0f) return;

        // S�reyi geri say
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            // S�re dolunca Level2�ye ge�
            GameManager.Instance?.GoToNextLevel();
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
