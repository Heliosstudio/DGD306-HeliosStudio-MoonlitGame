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

    void Awake()
    {
        // Singleton kal�b�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

 
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
}
