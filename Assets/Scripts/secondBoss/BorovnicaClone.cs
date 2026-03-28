using UnityEngine;

public class BorovnicaClone : MonoBehaviour
{
    
    [SerializeField] GameObject MinionPrefab;
    [SerializeField] float SpawnInterval = 4f;
    private float timer = 0f;

    private HealthComponent myHealth;

    public float amplitude = 0.9f; 
    public float frequency = 1f;   

    private Vector3 startPos;

    void Start()
    {
        myHealth = GetComponent<HealthComponent>();
        startPos = transform.position;
    }

    
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
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
