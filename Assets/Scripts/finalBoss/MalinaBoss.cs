using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MalinaBoss : MonoBehaviour
{
    [SerializeField] GameObject malinaMinion;

    [SerializeField] float spawnCooldown = 3f;
    [SerializeField] float spawnReduction = 0.5f;
    [SerializeField] int healAmount = 2;

    private HealthComponent malinaHealth;
    private RandomMovement mover;
    private ShootAtPlayer shooter;
    private Collider2D bossCollider;

    private bool isSpawning = true;
    private bool finalPhase = false;
    private bool bossStart = true;

    void Start()
    {
        malinaHealth = GetComponent<HealthComponent>();
        bossCollider = GetComponent<Collider2D>();
        bossCollider.enabled = false;

        mover = GetComponent<RandomMovement>();
        shooter = GetComponent<ShootAtPlayer>();

        StartCoroutine(BossLogicRoutine());
    }

    IEnumerator BossLogicRoutine() {
        while (true) {
            if (bossStart) {
                bossStart = false;
                yield return new WaitForEndOfFrame();
            }

            if (isSpawning) {
                SpawnMinions();
                yield return new WaitForSeconds(spawnCooldown -= spawnReduction);
            }

            if (malinaHealth.GetHealth() <= 4 && !finalPhase) {
                isSpawning = false;
                finalPhase = true;
                //StartBulletHell();

                GameObject[] minions = GameObject.FindGameObjectsWithTag("MalinaMinion");

                if (minions.Length > 0) {
                    int totalHeal = minions.Length * healAmount;
                    malinaHealth.Heal(totalHeal);
                }

                foreach (var minion in minions) {
                    Destroy(minion);
                }
            }

            if (finalPhase) {
                bossCollider.enabled = true;
                if (mover != null) mover.StartMoving();
                if (shooter != null) shooter.StartShooting();
                yield break;
            }

            yield return null;
        }
    }

    void SpawnMinions() {
        // malina boss spawnuje dva miniona jednog s leve i jednog s desne strane i to u nekim radnom pozicijama na vrhu ekrana

        malinaHealth.TakeDamage(4);

        Vector3 minionLeft = new Vector3(Random.Range(5f, 12.5f), 0, 0);
        Vector3 minionRight = new Vector3(Random.Range(5f, 12.5f), 0, 0);

        Instantiate(malinaMinion, transform.position - minionLeft, Quaternion.identity);
        Instantiate(malinaMinion, transform.position + minionRight, Quaternion.identity);
    }

    void StartBulletHell() {
        Debug.Log("pinuninuist");
        SceneManager.LoadScene("BulletHell", LoadSceneMode.Additive);
    }
}
