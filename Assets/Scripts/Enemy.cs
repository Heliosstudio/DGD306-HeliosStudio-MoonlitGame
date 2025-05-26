using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;        // Yukarý-aþaðý yavaþ hareket
    public float verticalRange = 0.5f;      // Ne kadar aþaðý yukarý
    private float originalY;
    private float timeOffset;

    void Start()
    {
        originalY = transform.position.y;
        timeOffset = Random.Range(0f, 2f * Mathf.PI); // farklýlaþma
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
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
