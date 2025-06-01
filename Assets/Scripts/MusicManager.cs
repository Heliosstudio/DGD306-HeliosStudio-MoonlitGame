using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gameplayMusic;

    private void Awake()
    {
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scene1" || scene.name == "Scene2" || scene.name == "Scene3")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = gameplayMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            // Menü sahnelerinde müziði durdur
            audioSource.Stop();
        }
    }
}
