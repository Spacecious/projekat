using System;
using System.Collections.Generic;
using UnityEngine;


public class BorovnicaBoss : MonoBehaviour
{
    public Animator animator;

    [SerializeField] GameObject MinionPrefab;
    [SerializeField] float SpawnInterval = 4f;
    private float timer = 0f;

    [SerializeField] GameObject smokeBomb;

    [SerializeField] GameObject ClonePrefab;
    [SerializeField] int NumberOfClones = 2;
    [SerializeField] float HPStep = 5f;

    private HealthComponent myHealth;
    private float nextHealthThreshold;

    public float amplitude = 0.8f; 
    public float frequency = 1f;
    private Vector3 startPos;

    [SerializeField] private AudioSource summon;

    void Start()
    {
        myHealth = GetComponent<HealthComponent>();
        nextHealthThreshold = myHealth.GetHealth() - HPStep;
        animator = GetComponent<Animator>();
        startPos = transform.position;
        SpawnMinion();
    }


    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        Vector3 pos = transform.position;
        pos.y = startPos.y + yOffset;
        transform.position = pos;

        timer += Time.deltaTime;

        if (timer >= SpawnInterval)
        {
            animator.SetTrigger("Attack");
            Invoke("SpawnMinion", 1f);
            timer = 0;
        }

        CheckForCloneAbility(myHealth.GetHealth()); 
    }


    void SpawnMinion()
    {
        
        if(MinionPrefab != null)
        {
            
            Instantiate(MinionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Borovnica je poslala miniona");
            
        }
    }

    public void CheckForCloneAbility(float HpBorovnica)
    {
        if(HpBorovnica <= nextHealthThreshold)
        {
            
            SpawnClones();
            nextHealthThreshold -= HPStep;
            Debug.Log("Klonovi stvoreni! Sledeći prag: " + nextHealthThreshold);
        }
    }
   

    void SpawnClones()
    {
        summon.Play();
        float spacing = 12.5f;
        

        
        for (int i = 0; i < NumberOfClones; i++)
        {
            float xOffset = (i - (NumberOfClones - 1) / 2f) * spacing;

            Vector3 spawnPosition = new Vector3(
                transform.position.x + xOffset,
                startPos.y,
                transform.position.z
            );
            Instantiate(smokeBomb, spawnPosition, Quaternion.identity);
            Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
