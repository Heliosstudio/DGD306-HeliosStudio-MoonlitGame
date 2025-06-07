using UnityEngine;

public class EnemyTypeB : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float verticalSpeed = 3f;
    public float verticalRange = 1f;
    public GameObject healthPickupPrefab;
    public float dropChance = 0.5f;

    private float originalY;
    private float timeOffset;
    private bool isDead = false;

    void Start()
    {
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
        if (isDead) return;

        if (other.CompareTag("Bullet"))
        {
            isDead = true;

            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                ScoreManager.Instance.AddScore(20, bullet.ownerPlayerId);

            }

            Destroy(other.gameObject);
            DropHealth();
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            isDead = true;

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(2);
            }

            Destroy(gameObject);
        }
    }

    void DropHealth()
    {
        if (Random.value <= dropChance && healthPickupPrefab != null)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
    }
}
