using System.Collections;
using UnityEngine;

public class MalinaBoss : MonoBehaviour
{
    [SerializeField] GameObject regularMinion;
    [SerializeField] GameObject teleportMinion;

    [SerializeField] float spawnCooldown = 3f;

    private HealthComponent malinaHealth;

    void Start()
    {
        malinaHealth = GetComponent<HealthComponent>();
        StartCoroutine(BossLogicRoutine());
    }

    IEnumerator BossLogicRoutine() {
        while (true) {
            SpawnRegularMinions();

            if (malinaHealth.GetHealth() <= 100) {
                SpawnTeleportMinions();
            }

            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    void SpawnRegularMinions() {
        // malina boss spawnuje dva miniona jednog s leve i jednog s desne strane i to u nekim radnom pozicijama na vrhu ekrana

        malinaHealth.TakeDamage(4);

        Vector3 minionLeft = new Vector3(Random.Range(5f, 15f), 0, 0);
        Vector3 minionRight = new Vector3(Random.Range(5f, 15f), 0, 0);

        Instantiate(regularMinion, transform.position - minionLeft, Quaternion.identity);
        Instantiate(regularMinion, transform.position + minionRight, Quaternion.identity);
    }

    void SpawnTeleportMinions() { 

    }


    void Update()
    {
        
    }
}
