using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class BossController : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Movement")]
    public float verticalSpeed = 2f;
    public float verticalRange = 3f;
    private float originalY;

    [Header("Shooting")]
    public GameObject bossBulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        originalY = transform.position.y;
    }

    void Update()
    {
        // Y ekseninde salýným hareketi
        float newY = originalY + Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Ateþ periyodu
        if (Time.time >= nextFireTime)
        {
            ShootPattern();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootPattern()
    {
        // Hedef: en yakýn oyuncu
        var players = GameObject.FindGameObjectsWithTag("Player")
                                .Select(go => go.transform)
                                .ToList();
        if (players.Count == 0) return;

        Transform target = players.OrderBy(p => Vector2.Distance(transform.position, p.position)).First();
        Vector2 dir = (target.position - transform.position).normalized;

        // Ortadaki mermi
        SpawnBossBullet(dir);

        // ±20° sapmalý ikiþer yan ateþ
        float angle = 20f * Mathf.Deg2Rad;
        Vector2 dirR = new Vector2(
            dir.x * Mathf.Cos(angle) - dir.y * Mathf.Sin(angle),
            dir.x * Mathf.Sin(angle) + dir.y * Mathf.Cos(angle)
        );
        Vector2 dirL = new Vector2(
            dir.x * Mathf.Cos(-angle) - dir.y * Mathf.Sin(-angle),
            dir.x * Mathf.Sin(-angle) + dir.y * Mathf.Cos(-angle)
        );
        SpawnBossBullet(dirR);
        SpawnBossBullet(dirL);
    }
    void SpawnBossBullet(Vector2 _)
    {
        Instantiate(bossBulletPrefab, firePoint.position, Quaternion.identity);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Oyuncu mermisi
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);   // her mermi 1 can eksiltir
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        // Örneðin WinScene’e geç
        SceneManager.LoadScene("WinScene");
        Destroy(gameObject);
    }
}
