using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Vector2 moveInput;

    void Update()
    {
        // Hareket
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    // Input System �zerinden gelen y�n verisi
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Ate�
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
