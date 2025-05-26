// Bullet.cs
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int ownerPlayerId = 1; 

    public float speed = 10f;

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
