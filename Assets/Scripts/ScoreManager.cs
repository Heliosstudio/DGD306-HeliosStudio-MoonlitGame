using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // �� skor de�erleri
    private int scoreP1 = 0;
    private int scoreP2 = 0;

    // Inspector�dan atad���n UI Text referanslar�
    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;

    // D��ar�ya okunabilir property�ler
    public int P1Score => scoreP1;
    public int P2Score => scoreP2;
    public static int TotalScore => Instance.scoreP1 + Instance.scoreP2;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("ScoreManager initialized and persisted.");
    }


    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount, int playerId)
    {
        if (playerId == 1)
            scoreP1 += amount;
        else if (playerId == 2)
            scoreP2 += amount;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreTextP1 != null)
            scoreTextP1.text = $"P1 Score: {scoreP1}";
        if (scoreTextP2 != null)
            scoreTextP2.text = $"P2 Score: {scoreP2}";
    }
    public void CheckAndSaveHighScore()
    {
        int previousHigh = PlayerPrefs.GetInt("HighScore", 0);
        int currentTotal = TotalScore;

        if (currentTotal > previousHigh)
        {
            PlayerPrefs.SetInt("HighScore", currentTotal);
            PlayerPrefs.Save();
            Debug.Log($"Yeni High Score: {currentTotal}");
        }
        else
        {
            Debug.Log($"High Score korunuyor: {previousHigh}");
        }
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    public void ResetScores()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        UpdateUI();
    }


}
