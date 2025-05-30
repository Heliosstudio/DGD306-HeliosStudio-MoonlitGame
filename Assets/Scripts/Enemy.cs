using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;
    public float verticalRange = 0.5f;
    public int maxHealth = 1;            // A türü için 1 can yeterli
    public int scoreValue = 10;          // Öldüðünde verilecek puan
    private int currentHealth;

    private float originalY;
    private float timeOffset;

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
            // Puan ekle
            var b = other.GetComponent<Bullet>();
            if (b != null)
                ScoreManager.Instance.AddScore(scoreValue, b.ownerPlayerId);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(1);

            Destroy(gameObject);
        }
    }
}
