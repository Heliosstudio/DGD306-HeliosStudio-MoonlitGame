using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPowerManager : MonoBehaviour
{
    // ID → Manager map’i
    public static Dictionary<int, PlayerPowerManager> Registry = new Dictionary<int, PlayerPowerManager>();

    [HideInInspector] public int playerId;
    private PlayerController p1Ctrl;
    private Player2Controller p2Ctrl;
    private PlayerHealth health;
    private float originalSpeed;

    void Awake()
    {
        // Kim olduğumuzu belirle
        if (GetComponent<PlayerController>() != null) playerId = 1;
        else if (GetComponent<Player2Controller>() != null) playerId = 2;

        // Registry’ye kaydet
        Registry[playerId] = this;

        p1Ctrl = GetComponent<PlayerController>();
        p2Ctrl = GetComponent<Player2Controller>();
        health = GetComponent<PlayerHealth>();

        if (p1Ctrl != null) originalSpeed = p1Ctrl.moveSpeed;
        else if (p2Ctrl != null) originalSpeed = p2Ctrl.moveSpeed;
    }

    public void ApplyPowerUp(PowerUp.PowerType type, float duration)
    {
        switch (type)
        {
            case PowerUp.PowerType.Shield:
                StartCoroutine(ActivateShield(duration));
                break;
            case PowerUp.PowerType.MultiShot:
                StartCoroutine(ActivateMultiShot(duration));
                break;
            case PowerUp.PowerType.Speed:
                StartCoroutine(ActivateSpeedBoost(duration));
                break;
        }
    }

    IEnumerator ActivateShield(float duration)
    {
        health.SetShield(true);
        yield return new WaitForSeconds(duration);
        health.SetShield(false);
    }

    IEnumerator ActivateMultiShot(float duration)
    {
        if (p1Ctrl != null) p1Ctrl.SetMultiShot(true);
        if (p2Ctrl != null) p2Ctrl.SetMultiShot(true);
        yield return new WaitForSeconds(duration);
        if (p1Ctrl != null) p1Ctrl.SetMultiShot(false);
        if (p2Ctrl != null) p2Ctrl.SetMultiShot(false);
    }

    IEnumerator ActivateSpeedBoost(float duration)
    {
        if (p1Ctrl != null) p1Ctrl.moveSpeed = originalSpeed * 1.5f;
        if (p2Ctrl != null) p2Ctrl.moveSpeed = originalSpeed * 1.5f;
        yield return new WaitForSeconds(duration);
        if (p1Ctrl != null) p1Ctrl.moveSpeed = originalSpeed;
        if (p2Ctrl != null) p2Ctrl.moveSpeed = originalSpeed;
    }
}
