using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;
    public float verticalRange = 0.5f;

    [Tooltip("Bu düþman 2 vuruþta ölsün")]
    public int maxHealth = 2;
    public int scoreValue = 10;

    private int currentHealth;
    private float originalY;
    private float timeOffset;
    [Header("HealthDrop")]
    public GameObject healthPickupPrefab;
    public float dropChance;
    void DropHealth()
    {
        if (Random.value <= dropChance && healthPickupPrefab != null)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        originalY = transform.position.y;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float newY = originalY + Mathf.Sin((Time.time + timeOffset) * verticalSpeed) * verticalRange;
        transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bulScript = other.GetComponent<Bullet>();
            TakeDamage(bulScript.damage, bulScript.ownerPlayerId);
            getSlow(bulScript.freezeAmount);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(1);
            Destroy(gameObject);
        }
    }
    void TakeDamage(int Damage,int bulletPlayer)
    {
        currentHealth -= Damage;
        if (currentHealth <= 0) { DropHealth(); Destroy(gameObject); ScoreManager.Instance.AddScore(scoreValue, bulletPlayer); }
    }
    void getSlow(float freezeAmount)
    {
        moveSpeed *= freezeAmount;
    }
}
