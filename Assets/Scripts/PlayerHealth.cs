using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private bool shieldEnabled = false;
    public void SetShield(bool v) => shieldEnabled = v;
    public int maxHealth = 10;
    public int currentHealth;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        if (shieldEnabled) return;        // Shield aktifse hasar yok

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthSlider != null) healthSlider.value = currentHealth;

        if (currentHealth <= 0) Die();
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
        Debug.Log("Oyuncu öldü!");
        Destroy(gameObject);
    }
}
