using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PathFinding : MonoBehaviour
{

    public float speed = 1.0f;
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



    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }
    }
    public void MoveTowardsPlayer()
    {
        speed = 1f;
        if (player == null) return;

        float direction = player.position.x > transform.position.x ? 1 : -1;
        float newX = transform.position.x + (direction * speed * Time.deltaTime);
        newX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }


    public void Attack(int attackCount)
    {
        if (attackCount % 5 == 0 && attackCount != 0)
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

        GameObject seed = Instantiate(seedPrefab, transform.position - new Vector3(0, 0.5f, 0), rotation);

        Rigidbody2D rbSeed  = seed.GetComponent<Rigidbody2D>();
        if(rbSeed != null)
        {
            float seedSpeed = Random.Range(1f, 4f);
            rbSeed.linearVelocity = seed.transform.up * seedSpeed;
        }
    }
}
