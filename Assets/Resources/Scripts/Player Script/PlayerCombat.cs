using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] AudioSource reloadSound;
    public int ammo = 5;
    public float damageMultiplier = 1f;

    private bool isReloading = false;
    private bool shootCooldown = true;
    private PlayerController controls; 
    private AmmoUI ammoUI;

    void Start()
    {
        controls = GetComponent<PlayerController>();
        ammoUI = Object.FindAnyObjectByType<AmmoUI>();
        if (ammoUI != null) ammoUI.UpdateAmmoDisplay(ammo);
    }

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
      
        if (ammo <= 0 || isReloading || !shootCooldown) return;

        Vector2 dir = Vector2.zero;
        bool isHoldingAnyKey = false;

      
        if (controls.fireUp.IsPressed()) { dir = Vector2.up * 10; isHoldingAnyKey = true; }
        else if (controls.fireDown.IsPressed()) { dir = Vector2.down * 10; isHoldingAnyKey = true; }
        else if (controls.fireLeft.IsPressed()) { dir = Vector2.left * 10; isHoldingAnyKey = true; }
        else if (controls.fireRight.IsPressed()) { dir = Vector2.right * 10; isHoldingAnyKey = true; }

        if (isHoldingAnyKey)
        {
            ExecuteShot(dir);
        }
    }

    void ExecuteShot(Vector2 dir)
    {
        GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
        p.GetComponent<Projectile>()?.setVelocity(dir, gameObject.tag);

        ammo--;
        ammoUI?.UpdateAmmoDisplay(ammo);

        shootCooldown = false;
        StartCoroutine(ShotCooldownRoutine());

        Destroy(p, 2f);

        if (ammo <= 0) StartCoroutine(ReloadRoutine());
    }

    IEnumerator ShotCooldownRoutine()
    {
      
        yield return new WaitForSeconds(0.5f);
        shootCooldown = true;
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true;
        shootCooldown = false; 
        yield return new WaitForSeconds(1f);

        reloadSound?.Play();
        ammo = 5;
        ammoUI?.UpdateAmmoDisplay(ammo);

        isReloading = false;
        shootCooldown = true;
    }
}