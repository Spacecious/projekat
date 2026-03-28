using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletHellEmitter : MonoBehaviour {
    [SerializeField] List<BulletPatternConfig> patterns;
    [SerializeField] float bulletOffset = 0f;

    void Start() {
        StartCoroutine(ExecuteAllPatterns());
    }

    IEnumerator ExecuteAllPatterns() {
        float offset = 0f;

        foreach (var pattern in patterns) {
            for (int i = 0; i < pattern.totalBursts; i++) {
                SpawnBulletBurst(pattern, offset);
                offset += bulletOffset;
                yield return new WaitForSeconds(pattern.timeBetweenBursts);
            }
            yield return new WaitForSeconds(1.5f);
        }

        Debug.Log("Preziveo si! Vracanje u glavni prozor...");
        SceneManager.UnloadSceneAsync("BulletHellScene");
    }

    void SpawnBulletBurst(BulletPatternConfig config, float offset) {
        float angleStep = (config.endAngle - config.startAngle) / (config.bulletCount - 1);

        for (int i = 0; i < config.bulletCount; i++) {
            float relativeAngle = config.startAngle + offset + (i * angleStep);
            float finalAngle = relativeAngle + transform.eulerAngles.z;
            float angleInRad = finalAngle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad));
            GameObject bullet = Instantiate(config.projectile, transform.position, Quaternion.identity);
            Projectile proj = bullet.GetComponent<Projectile>();

            if (proj != null) {
                proj.setVelocity(direction * config.bulletSpeed, "EnemyBullet");
            }
        }
    }
}
