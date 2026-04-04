using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;


public class PathFinding : MonoBehaviour
{

    public float speed = 7.0f;
    public float stopDistance = 1f;
    //[SerializeField] float pauseDuration = 3.0f;

    [SerializeField] float minX = -14f;
    [SerializeField] float maxX = 14f;

    public Transform player;
    //private float pauseTimer = 0f;
    //private bool isPaused = false;

    [SerializeField] GameObject seedPrefab;
    [SerializeField] int totalSeeds = 10;
    [SerializeField] float burstDuration = 3f;

    [SerializeField] GameObject jamPrefab;
    [SerializeField] int jamCount = 5;
    //private bool hasAttackedThisPause = false;
    //private int pauseCounter = 0;

    public float followSmoothness = 5f;

    [SerializeField] float damageDistance = 3f;
    [SerializeField] float damageCooldown = 1f;
    private float lastDamage = 0f;

    [SerializeField] GameObject Jagodica;

    [SerializeField] GameObject smokeBomb;



    void Start()
    {

        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); 
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

        void Update()
        {
            if (player != null)
            {
                MoveTowardsPlayer();
                Vector3 jagodicaPosition = Jagodica.transform.position;
                float currentDistance = Vector3.Distance(jagodicaPosition, player.position);
                if(currentDistance <= damageDistance)
                {
                    TryApllyDamage();
                }
            }
        }

    public void MoveTowardsPlayer()
    {

        if (player == null) return;
        speed = 2f;
        float direction = player.position.x > transform.position.x ? 1 : -1;
        float newX = transform.position.x + (direction * speed * Time.deltaTime);
        newX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }


    public void Attack(int attackCount)
    {
        Debug.Log("Attack called! Count: " + attackCount);
        if (attackCount % 2 == 0 && attackCount != 0)
        {
            StartCoroutine(JamSpamRoutine());
        }
        else
        {
            StartCoroutine(SeedBurstRoutine());
        }
    }

    IEnumerator SeedBurstRoutine()
    {
        float delayBetweenSeeds = burstDuration/ totalSeeds;
        for(int i  = 0; i < totalSeeds; i++)
        {
            SpawnSeed();
            yield return new WaitForSeconds(delayBetweenSeeds);
        }
    }

    IEnumerator JamSpamRoutine()
    {
        for(int i = 0; i < jamCount; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(-3, 0);
            Vector3 spawnPos = new Vector3(randomX, randomY, 0);
            Instantiate(jamPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(0.15f);
        }
    }

    
    void SpawnSeed()
    {
        float randomAngle = Random.Range(120f,240f);
        Quaternion rotation = Quaternion.Euler(0,0,randomAngle);

        GameObject seed = Instantiate(seedPrefab, transform.position, rotation);

        Rigidbody2D rbSeed  = seed.GetComponent<Rigidbody2D>();
        if(rbSeed != null)
        {
            float seedSpeed = Random.Range(1f, 4f);
            rbSeed.linearVelocity = seed.transform.up * seedSpeed;
        }
    }

    void TryApllyDamage()
    {
        if(Time.time >= lastDamage + damageCooldown)
        {
            Debug.Log("Jagoda te je udarila!");
            HealthComponent playerHealth = player.GetComponent<HealthComponent>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            lastDamage = Time.time;
        }
    }
}
