using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public GameObject fireballPrefab; // Artık P2 → Fireball
    public Transform firePoint;

    private Vector2 moveInput;
    private float specialCooldown = 5f;
    private float nextSpecialTime = 0f;

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().ownerPlayerId = 2;
        }
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextSpecialTime)
        {
            UseFireball();
            nextSpecialTime = Time.time + specialCooldown;
        }
    }

    private void UseFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = fireball.GetComponent<Bullet>();
        if (bullet != null) bullet.ownerPlayerId = 2;
    }
}
