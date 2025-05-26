using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;
    public float verticalRange = 0.5f;
    private float originalY;
    private float timeOffset;

    public float dropChance = 0.2f; // %20
    public GameObject healthPickupPrefab; 

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
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            DropHealth();
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
