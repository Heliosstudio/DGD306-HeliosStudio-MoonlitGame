using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 5f;

    void Start()
    {
        // lifetime saniye sonra otomatik silinsin
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Sağdan sola sabit hareket
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Sadece düşmanlara çarptığında hem onları hem de kendini yok et
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
