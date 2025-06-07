using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;
    public float verticalRange = 0.5f;

    [Tooltip("Bu d��man 2 vuru�ta �ls�n")]
    public int maxHealth = 2;
    public int scoreValue = 10;

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
            currentHealth--;
            Destroy(other.gameObject);
            if (currentHealth > 0)
                return;
            var b = other.GetComponent<Bullet>();
            if (b != null)
                ScoreManager.Instance.AddScore(scoreValue, b.ownerPlayerId);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
