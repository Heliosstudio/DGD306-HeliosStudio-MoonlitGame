using UnityEngine;

public class Orb : MonoBehaviour
{
    public GameObject[] powerUps; // Boost prefablarý
    public float dropChance = 0.5f; // %50 þans

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TryDropPowerUp();
            Destroy(gameObject);
        }
    }

    void TryDropPowerUp()
    {
        if (Random.value <= dropChance && powerUps.Length > 0)
        {
            int randomIndex = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
