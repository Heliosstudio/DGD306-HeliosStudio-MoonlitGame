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

    [Header("Audio")]
    public AudioClip fireSound;
    private AudioSource audioSource;

    private Vector2 moveInput;
    public Vector2 Border1, Border2;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //ekran oranına göre borderX
        Border1.x = (float)Screen.width / Screen.height * Border1.y;
        Border2.x = (float)Screen.width / Screen.height * Border2.y;
    }


    void Update()
    {
        #region BorderControl
        float playerX_ = transform.position.x + (moveInput * moveSpeed * Time.deltaTime).x;
        float playerY_ = transform.position.y + (moveInput * moveSpeed * Time.deltaTime).y;
        if (!(Border1.x >= playerX_ || Border2.x <= playerX_))
        {
            transform.Translate(Vector2.right * (moveInput.x * moveSpeed * Time.deltaTime));
        }
        if (!(Border2.y <= playerY_ || Border1.y >= playerY_))
        {
            transform.Translate(Vector2.up * (moveInput.y * moveSpeed * Time.deltaTime));
        }
        #endregion
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
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
