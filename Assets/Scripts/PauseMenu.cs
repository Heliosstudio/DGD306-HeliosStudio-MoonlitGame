using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel;

    private bool isPaused = false;

#if ENABLE_INPUT_SYSTEM
    private PlayerInput playerInput;
    private System.Action<InputAction.CallbackContext> pauseHandler;
#endif

    void Awake()
    {
#if ENABLE_INPUT_SYSTEM
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            pauseHandler = ctx => TogglePause(); // ✅ referansı tuttuğumuz handler
            playerInput.actions["Pause"].performed += pauseHandler;
        }
#endif
    }

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false); // Sahne başında panel gizli başlasın
    }

    void OnDestroy()
    {
#if ENABLE_INPUT_SYSTEM
        if (playerInput != null && pauseHandler != null)
        {
            playerInput.actions["Pause"].performed -= pauseHandler;
        }
#endif
    }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("ESC basıldı!");
            TogglePause();
        }
#else
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC basıldı!");
            TogglePause();
        }
#endif
    }

    private void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
