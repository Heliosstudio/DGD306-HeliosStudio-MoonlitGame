using UnityEngine;

public class Orb : MonoBehaviour
{
    public float boostDuration = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;

        Bullet b = other.GetComponent<Bullet>();
        if (b != null && PlayerPowerManager.Registry.TryGetValue(b.ownerPlayerId, out var mgr))
        {
            // Hangi oyuncunun mermisi ise ona Speed + MultiShot
            print("PowerUp");
            mgr.ApplyPowerUp(PowerUp.PowerType.Speed, boostDuration);
            mgr.ApplyPowerUp(PowerUp.PowerType.MultiShot, boostDuration);
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
