using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    [SerializeField] float speed = 3.0f;
    [SerializeField] float stopDistance = 1.0f;
    [SerializeField] float pauseDuration = 2.0f;

    [SerializeField] float minX = -14f;
    [SerializeField] float maxX = 14f;

    private Transform player;
    private float pauseTimer = 0f;
    private bool isPaused = false;

    [SerializeField] GameObject seedPrefab;
    [SerializeField] int totalSeeds = 20;
    [SerializeField] float burstDuration = 2f;

    [SerializeField] GameObject jamPrefab;
    [SerializeField] int jamCount = 5;
    private bool hasAttackedThisPause = false;
    private int pauseCounter = 0;



    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    
    void Update()
    {
        if(player == null)
        {
            return;
        }
        if (isPaused)
        {
            HandlePause();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    public void HandlePause()
    {
        if (!hasAttackedThisPause)
        {
            if(pauseCounter % 5 == 0 && pauseCounter != 0)
            {
                StartCoroutine(JamSpamRoutine());
            }
            else
            {
                StartCoroutine(SeedBurstRoutine());
            }
            hasAttackedThisPause = true;
        }

        pauseTimer -= Time.deltaTime;
        if(pauseTimer <= 0)
        {
            isPaused = false;
            hasAttackedThisPause = false;
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
            float randomY = Random.Range(0, 10);
            Vector3 spawnPos = new Vector3(randomX, -3, 0);
            Instantiate(jamPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(0.15f);
        }
    }

    void SpawnSeed()
    {
        float randomAngle = Random.Range(90f,270f);
        Quaternion rotation = Quaternion.Euler(0,0,randomAngle);

        GameObject seed = Instantiate(seedPrefab, transform.position, rotation);

        Rigidbody2D rbSeed  = seed.GetComponent<Rigidbody2D>();
        if(rbSeed != null)
        {
            float seedSpeed = 5f;
            rbSeed.linearVelocity = seed.transform.up * seedSpeed;
        }
    }

    public void MoveTowardsPlayer()
    {
        float distanceFromPlayer = Mathf.Abs(player.position.x - transform.position.x);
        if(distanceFromPlayer <= stopDistance)
        {
            StartPause();
            return;
        }

        float direction = player.position.x > transform.position.x ? 1 : -1;
        float newX = transform.position.x + (direction*speed*Time.deltaTime);

        newX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void StartPause()
    {
        isPaused = true;
        pauseTimer = pauseDuration;
        pauseCounter++;
    }

   
}
