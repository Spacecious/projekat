using UnityEngine;

public class DeathProjectile : MonoBehaviour
{
    [Header("Postavke Štete")]
    [SerializeField] int damage = 3; 
    [SerializeField] float lifetime = 5f; 

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
 
        if (other.CompareTag("Player"))
        {
            HealthComponent playerHealth = other.GetComponent<HealthComponent>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("GIGANTSKI PROJEKTIL JE POGODIO IGRAcA! Instant smrt.");
            }
        }


       
    }
}
