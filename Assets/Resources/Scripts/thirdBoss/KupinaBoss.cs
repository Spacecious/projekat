using System.Collections;
using UnityEngine;

public class KupinaBoss : MonoBehaviour
{
    
    [SerializeField] GameObject granataPrefab;
    [SerializeField] float spwanRate = 4f;

    [SerializeField] GameObject vinePrefab;
    [SerializeField] float vineInterval = 6f;

    public Animator animator;

    [SerializeField] int minVines = 3;
    [SerializeField] int maxVines = 6;

    [SerializeField] float minX; 
    [SerializeField] float maxX; 
    [SerializeField] float minY; 
    [SerializeField] float maxY;

    [SerializeField] float shieldCooldown = 10f;
    [SerializeField] float shieldDuration = 3f;
    [SerializeField] float reflectDamage = 20f;

    

    private HealthComponent myHealth;
    private int currentNumberOfVines;
    public bool isShieldActive { get; private set; } = false;

    private float shieldTimer = 0f;
    private float timer = 0f;
    private float vineTimer = 0f;
    private float maxHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        myHealth = GetComponent<HealthComponent>();
        maxHealth = myHealth.GetHealth();
        currentNumberOfVines = minVines;
        ShootGranata();
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spwanRate)
        {
            ShootGranata();
            timer = 0f;
        }

        vineTimer += Time.deltaTime;
        if(vineTimer >= vineInterval)
        {
            SpawnVineTraps();
            vineTimer = 0;
        }

    }

    


    void ShootGranata()
    {
        animator.SetTrigger("Granata");
        if (granataPrefab != null)
        {
            
            Instantiate(granataPrefab, transform.position, Quaternion.identity);
            Debug.Log("Kupina je ispalila granatu!");
        }
    }

    void SpawnVineTraps()
    {
        animator.SetTrigger("Vine");
        for (int i = 0; i < currentNumberOfVines; i++)
        {
            float randX = Random.Range(minX, maxX);
            float randY = Random.Range(minY, maxY);

            Vector3 spawnPos = new Vector3(randX, randY, 0);
            Instantiate(vinePrefab, spawnPos, Quaternion.identity); 
        }
    }

    public void UpdateVineCount(float currentHP)
    {
        if (maxHealth <= 0)
        {
            maxHealth = myHealth.GetHealth();
            if (maxHealth <= 0) return;
        }
        float healthPercentLost = 1f - (currentHP / (float)maxHealth);
        currentNumberOfVines = Mathf.RoundToInt(Mathf.Lerp(minVines, maxVines, healthPercentLost));

        Debug.Log("Kupina je besna! Trenutni broj loza: " + currentNumberOfVines);
    }
}
