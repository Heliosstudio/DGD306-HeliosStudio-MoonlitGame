using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowHighScores()
    {
        int p1 = ScoreManager.Instance.P1Score;
        int p2 = ScoreManager.Instance.P2Score;
        int total = ScoreManager.TotalScore;

        // Eski bireysel skorlar saklanmaya devam ediyorsa:
        int best1 = PlayerPrefs.GetInt("BestP1", 0);
        int best2 = PlayerPrefs.GetInt("BestP2", 0);
        PlayerPrefs.SetInt("BestP1", Mathf.Max(best1, p1));
        PlayerPrefs.SetInt("BestP2", Mathf.Max(best2, p2));

        // Toplam skor i�in high score kontrol�
        int bestTotal = PlayerPrefs.GetInt("HighScore", 0);
        if (total > bestTotal)
        {
            PlayerPrefs.SetInt("HighScore", total);
        }

        PlayerPrefs.Save();

        SceneManager.LoadScene("HighScoresScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
