using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float MaxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = MaxHealth;    
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(float dagame)
    {
        currentHealth = Mathf.Max(currentHealth - dagame, 0);
        if(currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
