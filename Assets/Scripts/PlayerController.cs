using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 5f;
    private bool multiShotEnabled = false;
    public void SetMultiShot(bool v) => multiShotEnabled = v;

    [Header("Prefabs & Points")]
    public GameObject bulletPrefab;
    public GameObject iceBlastPrefab;
    public Transform firePoint;

    [Header("Special (Ice Blast)")]
    public float specialCooldown = 2f;
    private float nextSpecialTime = 0f;

    private Vector2 moveInput;

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
        if (!context.performed) return;


        if (multiShotEnabled)
        {
            SpawnBullet(firePoint.position + Vector3.up * 0.2f, 1);
            SpawnBullet(firePoint.position, 1);
            SpawnBullet(firePoint.position + Vector3.down * 0.2f, 1);
        }
        else
        {
            SpawnBullet(firePoint.position, 1);
        }
    }

    public void OnSpecial(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (GameManager.Instance.currentLevelIndex < 3) return;
        if (Time.time < nextSpecialTime) return;

        // IceBlast instantiate
        var go = Instantiate(iceBlastPrefab, firePoint.position, Quaternion.identity);
        var b = go.GetComponent<Bullet>();
        if (b != null) b.ownerPlayerId = 1;

        nextSpecialTime = Time.time + specialCooldown;
    }


    private void SpawnBullet(Vector3 pos, int ownerId)
    {
        var go = Instantiate(bulletPrefab, pos, Quaternion.identity);
        var b = go.GetComponent<Bullet>();
        if (b != null) b.ownerPlayerId = ownerId;
    }
}
