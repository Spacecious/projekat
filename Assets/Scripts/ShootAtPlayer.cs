using System.Collections;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private Transform player;

    [SerializeField] private float reloadSpeed = 1f;
    [SerializeField] private int ammo = 3;
    [SerializeField] private float shootInterval = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    public void StartShooting() {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine() {
        while (true) {
            int magazine = ammo;
            while (magazine > 0 && player != null) {
                Shoot();
                magazine--;
                yield return new WaitForSeconds(shootInterval);
            }

            yield return new WaitForSeconds(reloadSpeed);
        }
    }

    void Shoot() {
        if (projectile == null || player == null) return;

        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile proj = bullet.GetComponent<Projectile>();

        Vector2 direction = (player.position - transform.position).normalized;

        proj.setVelocity(bulletSpeed * direction, gameObject.tag);
    }
}
