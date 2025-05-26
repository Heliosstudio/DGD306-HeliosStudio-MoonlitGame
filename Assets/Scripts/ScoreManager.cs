using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int scoreP1 = 0;
    public int scoreP2 = 0;

    public TextMeshProUGUI scoreTextP1;
    public TextMeshProUGUI scoreTextP2;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddScore(int amount, int playerId)
    {
        if (playerId == 1)
        {
            scoreP1 += amount;
            if (scoreTextP1 != null)
                scoreTextP1.text = "P1 Score: " + scoreP1;
        }
        else if (playerId == 2)
        {
            scoreP2 += amount;
            if (scoreTextP2 != null)
                scoreTextP2.text = "P2 Score: " + scoreP2;
        }
    }
}
