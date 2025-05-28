using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerType { Shield, MultiShot, Speed }
    public PowerType powerType;
    public float duration = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var mgr = other.GetComponent<PlayerPowerManager>();
            if (mgr != null)
            {
                mgr.ApplyPowerUp(powerType, duration);
                Destroy(gameObject);
            }
        }
    }
}
