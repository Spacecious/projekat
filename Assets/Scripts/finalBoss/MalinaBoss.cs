using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MalinaBoss : MonoBehaviour
{
    [SerializeField] GameObject malinaMinion;

    [SerializeField] float spawnCooldown = 3f;

    private HealthComponent malinaHealth;
    private bool isSpawning = true;

    void Start()
    {
        malinaHealth = GetComponent<HealthComponent>();
        StartCoroutine(BossLogicRoutine());
    }

    IEnumerator BossLogicRoutine() {
        while (true) {
            if (isSpawning)
                SpawnMinions();

            if (malinaHealth.GetHealth() <= 4) {
                isSpawning = false;
                StartBulletHell();
            }

            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    void SpawnMinions() {
        // malina boss spawnuje dva miniona jednog s leve i jednog s desne strane i to u nekim radnom pozicijama na vrhu ekrana

        malinaHealth.TakeDamage(4);

        Vector3 minionLeft = new Vector3(Random.Range(5f, 15f), 0, 0);
        Vector3 minionRight = new Vector3(Random.Range(5f, 15f), 0, 0);

        Instantiate(malinaMinion, transform.position - minionLeft, Quaternion.identity);
        Instantiate(malinaMinion, transform.position + minionRight, Quaternion.identity);
    }

    void StartBulletHell() {
        SceneManager.LoadScene("BulletHell", LoadSceneMode.Additive);
    }
}
