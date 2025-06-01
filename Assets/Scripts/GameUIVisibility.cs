using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIVisibility : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public GameObject[] uiElementsToToggle; // HealthBar, ScoreText, TimerText, PausePanel vb.

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateVisibility(SceneManager.GetActiveScene());
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateVisibility(scene);
    }

    void UpdateVisibility(Scene scene)
    {
        bool show = scene.name == "Scene1" || scene.name == "Scene2" || scene.name == "Scene3";

        foreach (var ui in uiElementsToToggle)
        {
            if (ui != null)
                ui.SetActive(show);
        }
    }
}
