using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AplleMaster : MonoBehaviour
{

    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float grenadeCooldown = 1f;
    private float lastGrenadeTime = -1f;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletCount = 10;
    [SerializeField] float spreadAngle = 60f;
    [SerializeField] float burstCooldown = 2f;
    private float lastBurstTime = -1f;

    void Update()
    {
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            TryThrowGrenande();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TrySeedBurst();
        }
    }

    void TryThrowGrenande()
    {
        if(Time.time >= lastGrenadeTime + grenadeCooldown)
        {
            ThrowGrenade();
            lastGrenadeTime = Time.time;
        }
    }

    void ThrowGrenade()
    {
        if (grenadePrefab != null)
        {
            Instantiate(grenadePrefab, transform.position, Quaternion.identity);
        }
    }

    void TrySeedBurst()
    {
        if (Time.time >= lastBurstTime + burstCooldown)
        {
            StartCoroutine(SeedBurstRoutine());
            lastBurstTime = Time.time;
        }
    }

    IEnumerator SeedBurstRoutine()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + randomAngle);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
