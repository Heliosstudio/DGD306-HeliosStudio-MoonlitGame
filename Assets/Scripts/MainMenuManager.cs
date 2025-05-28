using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scene1"); // Level1 sahnesine ge�i�
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits"); // Credits sahnesi
    }

    public void QuitGame()
    {
        Application.Quit(); // Sadece build'de �al���r
        Debug.Log("Oyun kapat�l�yor...");
    }
}
