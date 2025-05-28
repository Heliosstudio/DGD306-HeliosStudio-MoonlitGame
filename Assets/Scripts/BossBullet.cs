using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 6f;
    [HideInInspector] public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(2); // boss mermisi 2 can götürsün

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Orb"))
        {
            Destroy(gameObject); // duvara veya baþka objeye çarpýnca sil
        }
    }
}
