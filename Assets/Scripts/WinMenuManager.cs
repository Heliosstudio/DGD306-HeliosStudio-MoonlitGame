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
        SceneManager.LoadScene("HighScoresScene");
 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatýlýyor...");
    }
}
