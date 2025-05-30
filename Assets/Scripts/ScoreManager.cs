using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // Ýç skor deðerleri
    private int scoreP1 = 0;
    private int scoreP2 = 0;

    // Inspector’dan atadýðýn UI Text referanslarý
    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;

    // Dýþarýya okunabilir property’ler
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
        DontDestroyOnLoad(gameObject); // Level geçiþlerinde kaybolmaz
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

    // High Score kontrolü ve kaydý
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

    // High Score'ý al
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    // Skorlarý sýfýrla (yeni oyun için)
    public void ResetScores()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        UpdateUI();
    }


}
