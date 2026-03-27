using UnityEngine;

public class SeedBullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float damage = 10f;
    [SerializeField] float lifeTime = 4f;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
        rb.linearVelocity = transform.up * speed;

        
        Destroy(gameObject, lifeTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            HealthComponent playerHealth = other.GetComponent<HealthComponent>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage);
            }

            Destroy(gameObject);
        }
    }
    
}
