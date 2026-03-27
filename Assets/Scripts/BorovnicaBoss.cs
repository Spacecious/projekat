using System;
using UnityEngine;
using System.Collections.Generic;

public class BorovnicaBoss : MonoBehaviour
{

    [SerializeField] GameObject MinionPrefab;
    [SerializeField] float SpawnInterval = 4f;
    private float timer = 0f;

    [SerializeField] GameObject ClonePrefab;
    [SerializeField] int NumberOfClones = 2;
    [SerializeField] float HPStep = 50f;

    private HealthComponent myHealth;
    private float nextHealthThreshold;
    
    void Start()
    {
        myHealth = GetComponent<HealthComponent>();
        nextHealthThreshold = myHealth.GetHealth() - HPStep;
        CheckForCloneAbility(1000);
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

    public void CheckForCloneAbility(float HpBorovnica)
    {
        if(HpBorovnica <= nextHealthThreshold || HpBorovnica == 1000)
        {
            SpawnClones();
            nextHealthThreshold -= HPStep;
            Debug.Log("Klonovi stvoreni! Sledeći prag: " + nextHealthThreshold);
        }
    }

    void SpawnClones()
    {
        float spacing = 12.5f;
        for (int i = 0; i < NumberOfClones; i++)
        {
            float xOffset = (i - (NumberOfClones - 1) / 2f) * spacing;
            
            Vector3 spawnPosition = new Vector3(transform.position.x + xOffset, transform.position.y, 0);

            Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
