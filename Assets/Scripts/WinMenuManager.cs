using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour
{
    // Main Menu�e d�n
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // High Scores sahnesini g�ster ve skorlar� kaydet
    public void ShowHighScores()
    {
        // Ge�erli oyun skorlar�
        int p1 = ScoreManager.Instance.P1Score;
        int p2 = ScoreManager.Instance.P2Score;

        // PlayerPrefs�ten �nceki en iyi de�erleri al
        int best1 = PlayerPrefs.GetInt("BestP1", 0);
        int best2 = PlayerPrefs.GetInt("BestP2", 0);

        // En y�ksek de�erleri sakla
        PlayerPrefs.SetInt("BestP1", Mathf.Max(best1, p1));
        PlayerPrefs.SetInt("BestP2", Mathf.Max(best2, p2));
        PlayerPrefs.Save();

        // HighScores sahnesine ge�
        SceneManager.LoadScene("HighScoresScene");
    }

    // Oyunu kapat
    public void QuitGame()
    {
        Application.Quit();
    }
}
