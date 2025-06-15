// Bullet.cs
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int ownerPlayerId = 1; 
    public int damage;
    public float speed;
    public float freezeAmount;
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
