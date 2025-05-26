using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;
    public float verticalRange = 0.5f;
    private float originalY;
    private float timeOffset;
    private bool isDead = false;

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
        if (isDead) return;

        if (other.CompareTag("Bullet"))
        {
            isDead = true;

            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                ScoreManager.Instance.AddScore(10, bullet.ownerPlayerId);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            isDead = true;

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
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
