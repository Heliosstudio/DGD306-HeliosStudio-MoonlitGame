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
            // Hasar uygula
            currentHealth--;

            // Mermiyi yok et
            Destroy(other.gameObject);

            // Eðer hâlâ yaþýyorsa hiçbir þey yapma
            if (currentHealth > 0)
                return;

            // Öldü: puan ekle
            var b = other.GetComponent<Bullet>();
            if (b != null)
                ScoreManager.Instance.AddScore(scoreValue, b.ownerPlayerId);

            // Düþmaný yok et
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            // Oyuncuya temasta bir can eksilt
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(1);

            // Düþman oyuncuya çarptýðýnda anýnda yok olsun
            Destroy(gameObject);
        }
    }
}
