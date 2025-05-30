using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoresManager : MonoBehaviour
{
    public TMP_Text bestP1Text;
    public TMP_Text bestP2Text;

    void Start()
    {
        int best1 = PlayerPrefs.GetInt("BestP1", 0);
        int best2 = PlayerPrefs.GetInt("BestP2", 0);

        bestP1Text.text = $"Best P1 Score: {best1}";
        bestP2Text.text = $"Best P2 Score: {best2}";
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
