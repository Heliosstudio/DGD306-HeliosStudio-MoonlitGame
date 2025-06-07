using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Tooltip("Oyun sahnelerindeki GameUI prefab’ýný buraya sürükle")]
    public GameObject gameUIPrefab;

    private GameObject gameUIInstance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Scene1" || scene.name == "Scene2" || scene.name == "Scene3")
        {

            if (gameUIInstance == null)
            {
                gameUIInstance = Instantiate(gameUIPrefab);
            }
            else
            {
                gameUIInstance.SetActive(true);
            }
        }
        else
        {
            if (gameUIInstance != null)
                gameUIInstance.SetActive(false);
        }
    }
}
