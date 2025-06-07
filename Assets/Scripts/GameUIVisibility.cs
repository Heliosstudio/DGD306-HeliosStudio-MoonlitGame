using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIVisibility : MonoBehaviour
{
    void OnEnable()
    {
        UpdateVisibility(SceneManager.GetActiveScene());
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (scene.name == "Scene1" || scene.name == "Scene2" || scene.name == "Scene3")
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
