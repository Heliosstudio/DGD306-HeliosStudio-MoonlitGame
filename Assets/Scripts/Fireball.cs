using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 12f;

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(enemy.maxHealth); // 💥 Direkt öldür
            }

            Destroy(gameObject);
        }
    }

}
