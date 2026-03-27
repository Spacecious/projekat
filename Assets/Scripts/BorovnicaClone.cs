using UnityEngine;

public class BorovnicaClone : MonoBehaviour
{
    
    [SerializeField] GameObject MinionPrefab;
    [SerializeField] float SpawnInterval = 4f;
    private float timer = 0f;

    private HealthComponent myHealth;

    void Start()
    {
        myHealth = GetComponent<HealthComponent>();
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= SpawnInterval)
        {
            SpawnMinion();
            timer = 0;
        }
    }

    void SpawnMinion()
    {
        if(MinionPrefab != null)
        {
            Instantiate(MinionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Borovnica je poslala miniona");
        }
    }
}
