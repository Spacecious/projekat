using UnityEngine;

public class AppleBullet : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    [SerializeField] float damage = 1f;
    [SerializeField] float lifeTime = 4f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.linearVelocity = transform.up * speed;
        
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            HealthComponent ClickyHealth = other.GetComponent<HealthComponent>();
            
            if (ClickyHealth != null)
            {
                ClickyHealth.TakeDamage((int)damage);
            }

            Destroy(gameObject);
        }
    }
}
