using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

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

    [Header("Audio")]
    public AudioClip fireSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        originalY = transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Y ekseninde sal�n�m hareketi
        float newY = originalY + Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Ate� periyodu
        if (Time.time >= nextFireTime)
        {
            ShootPattern();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootPattern()
    {
        // Hedef: en yak�n oyuncu
        var players = GameObject.FindGameObjectsWithTag("Player")
                                .Select(go => go.transform)
                                .ToList();
        if (players.Count == 0) return;

        Transform target = players.OrderBy(p => Vector2.Distance(transform.position, p.position)).First();
        Vector2 dir = (target.position - transform.position).normalized;

        // Ortadaki mermi
        SpawnBossBullet(dir);

        // �20� sapmal� iki�er yan ate�
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
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bulScript = other.GetComponent<Bullet>();
            TakeDamage(bulScript.damage);
            getSlow(bulScript.freezeAmount);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
           Die();
    }
    void getSlow(float freezeAmount)
    {
        verticalSpeed *= freezeAmount;
    }
    void Die()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(1000, 1);
            ScoreManager.Instance.AddScore(1000, 2);
        }
        GameManager.Instance.StartCoroutine(GameManager.Instance.Finish());
        Destroy(gameObject);
        
    }
    
}
