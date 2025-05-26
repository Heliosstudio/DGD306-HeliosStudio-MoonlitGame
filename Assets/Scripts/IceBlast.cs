using UnityEngine;

public class IceBlast : MonoBehaviour
{
    public float speed = 5f;

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
                enemy.moveSpeed *= 0.3f;
            }

            Destroy(gameObject);
        }
    }
}
