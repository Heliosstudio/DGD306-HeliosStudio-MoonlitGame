using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoresManager : MonoBehaviour
{
    public TextMeshProUGUI p1BestText;
    public TextMeshProUGUI p2BestText;
    public TextMeshProUGUI totalHighText;

    void Start()
    {
        int best1 = PlayerPrefs.GetInt("BestP1", 0);
        int best2 = PlayerPrefs.GetInt("BestP2", 0);
        int highTotal = PlayerPrefs.GetInt("HighScore", 0);

        p1BestText.text = "Best P1 Score: " + best1;
        p2BestText.text = "Best P2 Score: " + best2;
        totalHighText.text = "High Score (Total): " + highTotal;
    }


public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
