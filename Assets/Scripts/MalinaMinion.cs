using System.Collections;
using UnityEngine;

public class MalinaMinion : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    [SerializeField] private float paddingLeft = 0.5f;
    [SerializeField] private float paddingRight = 0.5f;
    [SerializeField] private float paddingTop = 0.5f;
    [SerializeField] private float paddingBottom = 0.5f;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float reloadSpeed = 1f;
    [SerializeField] private int ammo = 3;
    [SerializeField] private float shootInterval = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;

    private HealthComponent minionHealth;
    private Transform player;

    void Start()
    {
        minionHealth = GetComponent<HealthComponent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        InitBounds();
        StartCoroutine(MinionLogicRoutine());
    }

    private void InitBounds() {
        Camera camera = Camera.main;
        minBounds = camera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    IEnumerator MinionLogicRoutine() {
        while (true)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            float moveTimer = 0.5f;

            while (moveTimer > 0) {
                transform.position += randomDirection * moveSpeed * Time.deltaTime;
                ClampPosition();
                moveTimer -= Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            int magazine = ammo;
            while (magazine > 0 && player != null) {
                ShootAtPlayer();
                magazine--;
                yield return new WaitForSeconds(shootInterval);
            }

            yield return new WaitForSeconds(reloadSpeed);
        }
    }

    void ShootAtPlayer() {
        if (projectile == null || player == null) return;

        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile proj = bullet.GetComponent<Projectile>();

        Vector2 direction = (player.position - transform.position).normalized;

        proj.setVelocity(bulletSpeed * direction, gameObject.tag);
    }

    void ClampPosition() {
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
