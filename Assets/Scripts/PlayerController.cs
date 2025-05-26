using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public GameObject iceBlastPrefab; 
    public Transform firePoint;

    private Vector2 moveInput;
    private float specialCooldown = 2f;
    private float nextSpecialTime = 0f;

    void Update()
    {
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
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
            bullet.GetComponent<Bullet>().ownerPlayerId = 1;
        }
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextSpecialTime)
        {
            UseIceBlast();
            nextSpecialTime = Time.time + specialCooldown;
        }
    }

    private void UseIceBlast()
    {
        GameObject blast = Instantiate(iceBlastPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = blast.GetComponent<Bullet>();
        if (bullet != null) bullet.ownerPlayerId = 1;
    }
}
