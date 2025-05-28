using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene1"); // Level1 sahnesine geçiþ
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits"); // Credits sahnesi
    }

    public void QuitGame()
    {
        Application.Quit(); // Sadece build'de çalýþýr
        Debug.Log("Oyun kapatýlýyor...");
    }
}
