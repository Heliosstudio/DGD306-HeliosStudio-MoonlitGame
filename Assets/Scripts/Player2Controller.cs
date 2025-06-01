using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 5f;
    private bool multiShotEnabled = false;
    public void SetMultiShot(bool v) => multiShotEnabled = v;

    [Header("Prefabs & Points")]
    public GameObject bulletPrefab;
    public GameObject fireballPrefab;
    public Transform firePoint;

    [Header("Special (Fireball)")]
    public float specialCooldown = 5f;
    private float nextSpecialTime = 0f;

    private Vector2 moveInput;

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
        if (!context.performed) return;

        if (multiShotEnabled)
        {
            SpawnBullet(firePoint.position + Vector3.up * 0.2f, 2);
            SpawnBullet(firePoint.position, 2);
            SpawnBullet(firePoint.position + Vector3.down * 0.2f, 2);
        }
        else
        {
            SpawnBullet(firePoint.position, 2);
        }
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (GameManager.Instance.currentLevelIndex < 2) return;
        if (Time.time < nextSpecialTime) return;

        // Fireball instantiate inline:

        GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Vector2 dir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        fb.GetComponent<Fireball>().SetDirection(dir);

        nextSpecialTime = Time.time + specialCooldown;
    }


    private void SpawnBullet(Vector3 pos, int ownerId)
    {
        var go = Instantiate(bulletPrefab, pos, Quaternion.identity);
        var b = go.GetComponent<Bullet>();
        if (b != null) b.ownerPlayerId = ownerId;
    }
}
