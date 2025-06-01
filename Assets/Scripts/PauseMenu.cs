﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel;
    public Button resumeButton;

    private bool isPaused = false;

#if ENABLE_INPUT_SYSTEM
    private PlayerInput playerInput;
    private System.Action<InputAction.CallbackContext> pauseHandler;
    private string previousActionMap; // 🔁 Geri dönmek için
#endif

    void Awake()
    {
#if ENABLE_INPUT_SYSTEM
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            pauseHandler = ctx => TogglePause();
            playerInput.actions["Pause"].performed += pauseHandler;
        }
#endif
    }

    void Start()
    {
        // Eğer pausePanel sahne içinde yoksa tag ile bulmaya çalış
        if (pausePanel == null)
        {
            var found = GameObject.FindWithTag("PausePanel");
            if (found != null)
            {
                pausePanel = found;
                Debug.Log("PausePanel sahnede otomatik bulundu.");
            }
            else
            {
                Debug.LogWarning("PausePanel sahnede bulunamadı.");
            }
        }

        if (resumeButton == null)
        {
            var btnObj = GameObject.FindWithTag("ResumeButton");
            if (btnObj != null)
            {
                resumeButton = btnObj.GetComponent<Button>();
                Debug.Log("ResumeButton sahnede otomatik bulundu.");
            }
            else
            {
                Debug.LogWarning("ResumeButton sahnede bulunamadı.");
            }
        }

        if (pausePanel != null)
            pausePanel.SetActive(false);
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
            TogglePause();
        }
#else
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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

#if ENABLE_INPUT_SYSTEM
        if (playerInput != null && !string.IsNullOrEmpty(previousActionMap))
        {
            playerInput.SwitchCurrentActionMap(previousActionMap);
        }
#endif

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

#if ENABLE_INPUT_SYSTEM
        if (playerInput != null)
        {
            previousActionMap = playerInput.currentActionMap.name;
            playerInput.SwitchCurrentActionMap("UI");
        }
#endif

        if (resumeButton == null)
        {
            Debug.LogWarning("❌ Resume Button NULL!");
        }
        else if (!resumeButton.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("❌ Resume Button aktif değil!");
        }
        else
        {
            Debug.Log("✅ Resume Button seçiliyor...");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        }
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
