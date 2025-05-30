using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 5f;     // merminin ekrandan çýkmayý beklemeden ömrü

    void Start()
    {
        // lifetime saniye sonra otomatik silinsin
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // sabit sola hareket
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Sadece Player ile çarpýþýnca yok et ve hasar ver
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(1);  
        }

        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
