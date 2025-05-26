using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().ownerPlayerId = 2; // P2 olarak ayarlýyoruz
        }
    }


    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }
}
