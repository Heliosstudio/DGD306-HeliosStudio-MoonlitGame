using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Shield")]
    private bool shieldEnabled = false;
    public void SetShield(bool v) => shieldEnabled = v;

    [Header("Health Settings")]
    public int maxHealth = 10;
    public int currentHealth;

    [Header("UI")]
    [Tooltip("Bu oyuncunun slider'ına atanacak tag. Örneğin 'HealthSliderP1' veya 'HealthSliderP2'.")]
    public string sliderTag;
    private Slider healthSlider;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        // Sahne açıldığında slider referansını ayarla ve canı doldur
        AssignSlider();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        if (shieldEnabled) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} öldü!");

        // Bu oyuncu nesnesini yok et
        Destroy(gameObject);

        // GameManager’a oyuncunun öldüğünü bildir
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerDied();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Her sahne yüklendiğinde slider referansını tazele
        AssignSlider();

        // Eğer oyun sahnesiyse, canı tamamen doldur
        if (scene.name == "Scene1" || scene.name == "Scene2" || scene.name == "Scene3")
        {
            currentHealth = maxHealth;
            UpdateHealthUI();
        }
    }

    private void AssignSlider()
    {
        if (string.IsNullOrEmpty(sliderTag))
        {
            Debug.LogWarning($"{gameObject.name}: sliderTag boş bırakılmış.");
            healthSlider = null;
            return;
        }

        GameObject sliderObj = GameObject.FindWithTag(sliderTag);
        if (sliderObj != null)
        {
            healthSlider = sliderObj.GetComponent<Slider>();
            healthSlider.maxValue = maxHealth;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: '{sliderTag}' tag’li bir slider bulunamadı.");
            healthSlider = null;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }
}
