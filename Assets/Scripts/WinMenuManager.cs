using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour
{
    // Main Menu’e dön
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // High Scores sahnesini göster ve skorlarý kaydet
    public void ShowHighScores()
    {
        // Geçerli oyun skorlarý
        int p1 = ScoreManager.Instance.P1Score;
        int p2 = ScoreManager.Instance.P2Score;

        // PlayerPrefs’ten önceki en iyi deðerleri al
        int best1 = PlayerPrefs.GetInt("BestP1", 0);
        int best2 = PlayerPrefs.GetInt("BestP2", 0);

        // En yüksek deðerleri sakla
        PlayerPrefs.SetInt("BestP1", Mathf.Max(best1, p1));
        PlayerPrefs.SetInt("BestP2", Mathf.Max(best2, p2));
        PlayerPrefs.Save();

        // HighScores sahnesine geç
        SceneManager.LoadScene("HighScoresScene");
    }

    // Oyunu kapat
    public void QuitGame()
    {
        Application.Quit();
    }
}
