// Bullet.cs
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int ownerPlayerId = 1; 

    public float speed = 10f;

    void Start()
    {
        Debug.Log($"🔥 Bullet spawned by Player {ownerPlayerId}");
    }

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
